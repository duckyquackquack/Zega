namespace Zega
{
    public interface IInstructionGroup
    {
        byte Prefix { get; }
        void Execute(byte opCode);
    }
}
