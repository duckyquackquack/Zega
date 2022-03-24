namespace Zega
{
    public interface IMemory
    {
        byte ReadByte(ushort address);
        void WriteByte(ushort address, byte value);
    }
}
