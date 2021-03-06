namespace Zega.Cpu
{
    public class NonPrefixedInstructionGroup : BaseInstructionGroup
    {
        public NonPrefixedInstructionGroup(Dictionary<byte, Instruction> instructions)
            : base(instructions, 0x0)
        { }
    }

    public class CBInstructionGroup : BaseInstructionGroup
    {
        public CBInstructionGroup(Dictionary<byte, Instruction> instructions)
            : base(instructions, 0xCB)
        { }
    }

    public class DDInstructionGroup : BaseInstructionGroup
    {
        public DDInstructionGroup(Dictionary<byte, Instruction> instructions)
            : base(instructions, 0xDD)
        { }
    }

    public class FDInstructionGroup : BaseInstructionGroup
    {
        public FDInstructionGroup(Dictionary<byte, Instruction> instructions) 
            : base(instructions, 0xFD)
        { }
    }

    public class EDInstructionGroup : BaseInstructionGroup
    {
        public EDInstructionGroup(Dictionary<byte, Instruction> instructions)
            : base(instructions, 0xED)
        { }
    }

    public class DDCBInstructionGroup : BaseDisplacementInstructionGroup
    {
        public DDCBInstructionGroup(Dictionary<byte, DisplacementInstruction> instructions) 
            : base(instructions, 0xDD, 0xCB)
        { }
    }

    public class FDCBInstructionGroup : BaseDisplacementInstructionGroup
    {
        public FDCBInstructionGroup(Dictionary<byte, DisplacementInstruction> instructions)
            : base(instructions, 0xFD, 0xCB)
        { }
    }
}
