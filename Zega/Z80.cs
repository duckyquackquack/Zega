namespace Zega
{
    public partial class Z80
    {
        // TODO - see if we can make this private again. I don't like how it's public just so classes outside of Z80.cs,
        // namely the unit tests, can set and get them freely for initial setup and post-result Asserts
        public readonly Registers Registers;
        private readonly IMemory _memory;

        private readonly IList<IInstructionGroup> _prefixedInstructionGroups;
        private readonly IInstructionGroup _nonPrefixedInstructionGroup;

        public Z80(IMemory memory, Registers? registers = null)
        {
            _memory = memory ?? throw new ArgumentNullException(nameof(memory));
            Registers = registers ?? new Registers();
            
            _prefixedInstructionGroups = new List<IInstructionGroup>
            {
                new DDInstructionGroup(new Dictionary<byte, Instruction>())
            };

            _nonPrefixedInstructionGroup = new NonPrefixedInstructionGroup(new Dictionary<byte, Instruction>
            {
                { 0x80, new Instruction(/*Register, None, */AddAR, new List<int> { 4 }) },
                { 0x81, new Instruction(/*Register, None, */AddAR, new List<int> { 4 }) },
                { 0x82, new Instruction(/*Register, None, */AddAR, new List<int> { 4 }) },
                { 0x83, new Instruction(/*Register, None, */AddAR, new List<int> { 4 }) },
                { 0x84, new Instruction(/*Register, None, */AddAR, new List<int> { 4 }) },
                { 0x85, new Instruction(/*Register, None, */AddAR, new List<int> { 4 }) },
                { 0x87, new Instruction(/*Register, None, */AddAR, new List<int> { 4 }) }
            });
        }

        public void Step()
        {
            var opCode = _memory.ReadByte(Registers.ProgramCounter++);

            var prefixedInstructionGroup = _prefixedInstructionGroups.FirstOrDefault(x => x.Prefix == opCode);

            if (prefixedInstructionGroup == null)
                _nonPrefixedInstructionGroup.Execute(opCode);
            else
                prefixedInstructionGroup.Execute(opCode);
        }
    }
}
