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

        public uint Execute(byte opCode)
        {
            if (_instructions.TryGetValue(opCode, out var instruction))
            {
                instruction.Execute(opCode);
                return instruction.MCycles;
            }

            throw new Exception($"Unrecognized opCode 0x{opCode:X} (Prefix = 0x{Prefix:X})");
        }
    }
}
