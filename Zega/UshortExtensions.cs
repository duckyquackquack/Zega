namespace Zega
{
    public static class UshortExtensions
    {
        public static byte Hi(this ushort value) => (byte) (value >> 8);
        public static byte Lo(this ushort value) => (byte) (value & 255);
    }
}
