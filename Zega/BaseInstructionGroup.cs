namespace Zega
{
    public abstract class BaseInstructionGroup : IInstructionGroup
    {
        private readonly Dictionary<byte, Instruction> _instructions;

        protected BaseInstructionGroup(Dictionary<byte, Instruction> instructions, byte prefix)
        {
            _instructions = instructions ?? throw new ArgumentNullException(nameof(instructions));
            Prefix = prefix;
        }

        public byte Prefix { get; }

        public void Execute(byte opCode)
        {
            _instructions[opCode].Execute(opCode);
        }
    }
}
