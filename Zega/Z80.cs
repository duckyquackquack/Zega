namespace Zega
{
    public partial class Z80
    {
        // TODO - see if we can make this private again. I don't like how it's public just so classes outside of Z80.cs,
        // namely the unit tests, can set and get them freely for initial setup and post-result Asserts

        // TODO add halted bool
        // TODO add IFF1 and IFF2
        // TODO add interrupt mode enum

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
                new DDInstructionGroup(new Dictionary<byte, Instruction>
                {
                    { 0x46, new Instruction(LoadRIXD, 5, new List<int>{ 4, 4, 3, 5, 3 })},
                    { 0x4E, new Instruction(LoadRIXD, 5, new List<int>{ 4, 4, 3, 5, 3 })},
                    { 0x56, new Instruction(LoadRIXD, 5, new List<int>{ 4, 4, 3, 5, 3 })},
                    { 0x5E, new Instruction(LoadRIXD, 5, new List<int>{ 4, 4, 3, 5, 3 })},
                    { 0x66, new Instruction(LoadRIXD, 5, new List<int>{ 4, 4, 3, 5, 3 })},
                    { 0x6E, new Instruction(LoadRIXD, 5, new List<int>{ 4, 4, 3, 5, 3 })},
                    { 0x7E, new Instruction(LoadRIXD, 5, new List<int>{ 4, 4, 3, 5, 3 })}
                })
            };

            _nonPrefixedInstructionGroup = new NonPrefixedInstructionGroup(new Dictionary<byte, Instruction>
            {
                { 0x06, new Instruction(LoadRN, 2, new List<int>{ 4, 3 }) },
                { 0x0E, new Instruction(LoadRN, 2, new List<int>{ 4, 3 }) },
                { 0x16, new Instruction(LoadRN, 2, new List<int>{ 4, 3 }) },
                { 0x1E, new Instruction(LoadRN, 2, new List<int>{ 4, 3 }) },
                { 0x26, new Instruction(LoadRN, 2, new List<int>{ 4, 3 }) },
                { 0x2E, new Instruction(LoadRN, 2, new List<int>{ 4, 3 }) },
                { 0x3E, new Instruction(LoadRN, 2, new List<int>{ 4, 3 }) },

                { 0x40, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x41, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x42, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x43, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x44, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x45, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x46, new Instruction(LoadRHL, 2, new List<int> { 4, 3}) },
                { 0x47, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x48, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x49, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x4A, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x4B, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x4C, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x4D, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x4E, new Instruction(LoadRHL, 2, new List<int> { 4, 3}) },
                { 0x4F, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x50, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x51, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x52, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x53, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x54, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x55, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x56, new Instruction(LoadRHL, 2, new List<int> { 4, 3}) },
                { 0x57, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x58, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x59, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x5A, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x5B, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x5C, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x5D, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x5E, new Instruction(LoadRHL, 2, new List<int> { 4, 3}) },
                { 0x5F, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x60, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x61, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x62, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x63, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x64, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x65, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x66, new Instruction(LoadRHL, 2, new List<int> { 4, 3}) },
                { 0x67, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x68, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x69, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x6A, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x6B, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x6C, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x6D, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x6E, new Instruction(LoadRHL, 2, new List<int> { 4, 3}) },
                { 0x6F, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x78, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x79, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x7A, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x7B, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x7C, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x7D, new Instruction(LoadRR, 1, new List<int>{ 4 }) },
                { 0x7E, new Instruction(LoadRHL, 2, new List<int> { 4, 3}) },
                { 0x7F, new Instruction(LoadRR, 1, new List<int>{ 4 }) },

                { 0x80, new Instruction(AddAR, 1, new List<int> { 4 }) },
                { 0x81, new Instruction(AddAR, 1, new List<int> { 4 }) },
                { 0x82, new Instruction(AddAR, 1, new List<int> { 4 }) },
                { 0x83, new Instruction(AddAR, 1, new List<int> { 4 }) },
                { 0x84, new Instruction(AddAR, 1, new List<int> { 4 }) },
                { 0x85, new Instruction(AddAR, 1, new List<int> { 4 }) },
                { 0x87, new Instruction(AddAR, 1, new List<int> { 4 }) }
            });
        }

        public uint Step()
        {
            var opCode = _memory.ReadByte(Registers.ProgramCounter++);

            var prefixedInstructionGroup = _prefixedInstructionGroups.FirstOrDefault(x => x.Prefix == opCode);
            return prefixedInstructionGroup?.Execute(_memory.ReadByte(Registers.ProgramCounter)) 
                   ?? _nonPrefixedInstructionGroup.Execute(opCode);
        }
    }
}
