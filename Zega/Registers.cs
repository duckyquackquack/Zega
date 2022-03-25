namespace Zega
{
    public class Registers
    {
        // TODO - Set default values in ctor

        public byte A { get; set; }
        public byte B { get; set; }
        public byte C { get; set; }
        public byte D { get; set; }
        public byte E { get; set; }

        public Flags F
        {
            get => _f;
            set => _f = value;
        }

        public byte H { get; set; }
        public byte L { get; set; }

        public byte InterruptVector { get; set; }
        public byte MemoryRefresh { get; set; }

        public ushort IndexX { get; set; }
        public ushort IndexY { get; set; }
        public ushort StackPointer { get; set; }
        public ushort ProgramCounter { get; set; }

        public ushort BC => B.CombineWith(C);
        public ushort DE => D.CombineWith(E);
        public ushort HL => H.CombineWith(L);

        private byte _shadowA;
        private byte _shadowB;
        private byte _shadowC;
        private byte _shadowD;
        private byte _shadowE;
        private Flags _shadowF;
        private byte _shadowH;
        private byte _shadowL;

        // Private backing field so we can use the Extension methods on something that isn't just a copy
        private Flags _f;

        public void WriteToShadowRegisters()
        {
            _shadowA = A;
            _shadowB = B;
            _shadowC = C;
            _shadowD = D;
            _shadowE = E;
            _shadowF = F;
            _shadowH = H;
            _shadowL = L;
        }

        public void ReadFromShadowRegisters()
        {
            A = _shadowA;
            B = _shadowB;
            C = _shadowC;
            D = _shadowD;
            E = _shadowE;
            F = _shadowF;
            H = _shadowH;
            L = _shadowL;
        }

        public void SetFlag(Flags flagToSet, bool on)
        {
            if (on) _f.Set(flagToSet);
            else _f.Reset(flagToSet);
        }
    }
}
