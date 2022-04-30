namespace Zega.Cpu
{
    public interface IInstructionGroup
    {
        byte Prefix { get; }
        uint Execute(byte opCode);
    }

    public interface IDisplacementInstructionGroup
    {
        byte ParentGroupPrefix { get; }
        byte Prefix { get; }
        uint Execute(byte opCode, sbyte displacement);
    }
}
