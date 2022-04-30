namespace Zega.Cpu
{
    public abstract class BaseDisplacementInstructionGroup : IDisplacementInstructionGroup
    {
        private readonly Dictionary<byte, DisplacementInstruction> _instructions;

        protected BaseDisplacementInstructionGroup(Dictionary<byte, DisplacementInstruction> instructions, byte parentGroupPrefix, byte prefix)
        {
            _instructions = instructions;
            ParentGroupPrefix = parentGroupPrefix;
            Prefix = prefix;
        }

        public byte ParentGroupPrefix { get; }
        public byte Prefix { get; }

        public uint Execute(byte opCode, sbyte displacement)
        {
            if (_instructions.TryGetValue(opCode, out var instruction))
            {
                instruction.Execute(opCode, displacement);
                return (uint)instruction.TCycles.Sum();
            }

            throw new Exception($"Unrecognized opCode 0x{opCode:X} (Parent Prefix = 0x{ParentGroupPrefix:X}, Prefix = 0x{Prefix:X})");
        }
    }
}
