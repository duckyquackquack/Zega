namespace Zega
{
    public static class ByteExtensions
    {
        public static ushort CombineWith(this byte hi, byte lo) => (ushort) ((hi << 8) | lo);
    }
}
