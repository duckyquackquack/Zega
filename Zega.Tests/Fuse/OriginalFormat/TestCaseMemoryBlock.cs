namespace Zega.Tests.Fuse.OriginalFormat
{
    public class TestCaseMemoryBlock
    {
        public ushort StartAddress { get; set; }
        public List<byte> Bytes { get; set; } = new();
    }
}
