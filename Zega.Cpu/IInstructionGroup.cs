namespace Zega.Cpu
{
    public interface IInstructionGroup
    {
        byte Prefix { get; }
        uint Execute(byte opCode);
    }
}
