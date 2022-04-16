namespace Zega.Cpu.Tests.Fuse.OriginalFormat
{
    public class FuseEvent
    {
        public uint Time { get; set; } // The cycle-stamp at which this event occurs
        public FuseEventType EventType { get; set; }
        public ushort Address { get; set; }
        public byte? Data { get; set; }
    }
}
