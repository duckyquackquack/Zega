namespace Zega.Cpu.Tests.Fuse.OriginalFormat
{
    public class FuseExpectedCase
    {
        public string TestDescription { get; set; } = null!;
        public List<FuseEvent> Events { get; set; } = new();
        public ushort AF { get; set; }
        public ushort BC { get; set; }
        public ushort DE { get; set; }
        public ushort HL { get; set; }
        public ushort ShadowAF { get; set; }
        public ushort ShadowBC { get; set; }
        public ushort ShadowDE { get; set; }
        public ushort ShadowHL { get; set; }
        public ushort IndexX { get; set; }
        public ushort IndexY { get; set; }
        public ushort StackPointer { get; set; }
        public ushort ProgramCounter { get; set; }

        public byte InterruptVector { get; set; }
        public byte MemoryRefresh { get; set; }
        public bool InterruptFlipFlop1 { get; set; }
        public bool InterruptFlipFlop2 { get; set; }
        public byte InterruptMode { get; set; }
        public bool Halted { get; set; }

        public uint Cycles { get; set; } // Number of T-cycles the test should have ran

        public List<TestCaseMemoryBlock> ExpectedMemoryBlocks { get; set; } = new();
    }
}
