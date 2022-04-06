namespace Zega.Cpu
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

        public byte ShadowA { get; set; }
        public byte ShadowB { get; set; }
        public byte ShadowC { get; set; }
        public byte ShadowD { get; set; }
        public byte ShadowE { get; set; }
        public Flags ShadowF { get; set; }
        public byte ShadowH { get; set; }
        public byte ShadowL { get; set; }

        public byte InterruptVector { get; set; }
        public byte MemoryRefresh { get; set; }

        public ushort IndexX { get; set; }
        public ushort IndexY { get; set; }
        public ushort StackPointer { get; set; }
        public ushort ProgramCounter { get; set; }

        public ushort AF
        {
            get => A.CombineWith((byte)F);
            set
            {
                A = value.Hi();
                F = (Flags) value.Lo();
            }
        }

        public ushort BC
        {
            get => B.CombineWith(C);
            set
            {
                B = value.Hi();
                C = value.Lo();
            }
        }

        public ushort DE
        {
            get => D.CombineWith(E);
            set
            {
                D = value.Hi();
                E = value.Lo();
            }
        }

        public ushort HL
        {
            get => H.CombineWith(L);
            set
            {
                H = value.Hi();
                L = value.Lo();
            }
        }

        public ushort ShadowAF
        {
            get => ShadowA.CombineWith((byte)ShadowF);
            set
            {
                ShadowA = value.Hi();
                ShadowF = (Flags)value.Lo();
            }
        }

        public ushort ShadowBC
        {
            get => ShadowB.CombineWith(ShadowC);
            set
            {
                ShadowB = value.Hi();
                ShadowC = value.Lo();
            }
        }

        public ushort ShadowDE
        {
            get => ShadowD.CombineWith(ShadowE);
            set
            {
                ShadowD = value.Hi();
                ShadowE = value.Lo();
            }
        }

        public ushort ShadowHL
        {
            get => ShadowH.CombineWith(ShadowL);
            set
            {
                ShadowH = value.Hi();
                ShadowL = value.Lo();
            }
        }

        // Private backing field so we can use the Extension methods on something that isn't just a copy
        private Flags _f;

        public void WriteToShadowRegisters()
        {
            ShadowA = A;
            ShadowB = B;
            ShadowC = C;
            ShadowD = D;
            ShadowE = E;
            ShadowF = F;
            ShadowH = H;
            ShadowL = L;
        }

        public void ReadFromShadowRegisters()
        {
            A = ShadowA;
            B = ShadowB;
            C = ShadowC;
            D = ShadowD;
            E = ShadowE;
            F = ShadowF;
            H = ShadowH;
            L = ShadowL;
        }

        public void SetFlag(Flags flagToSet, bool on)
        {
            if (on) _f.Set(flagToSet);
            else _f.Reset(flagToSet);
        }
    }
}
