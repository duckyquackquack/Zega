namespace Zega
{
    public class NonPrefixedInstructionGroup : BaseInstructionGroup
    {
        public NonPrefixedInstructionGroup(Dictionary<byte, Instruction> instructions)
            : base(instructions, 0x0)
        { }
    }

    public class DDInstructionGroup : BaseInstructionGroup
    {
        public DDInstructionGroup(Dictionary<byte, Instruction> instructions)
            : base(instructions, 0xDD)
        { }
    }
}
