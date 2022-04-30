namespace Zega.Cpu
{
    public partial class Z80
    {
        private void NegateA(byte opCode)
        {
            var before = Registers.A;
            var negation = 0 - before;

            Registers.A = (byte)negation;

            Registers.SetFlag(Flags.Sign, (Registers.A & 0b10000000) > 0);
            Registers.SetFlag(Flags.Zero, Registers.A == 0);
            Registers.SetFlag(Flags.HalfCarry, (before & 0x0F) + ((~before + 1) & 0x0F) > 0xF); 
            Registers.SetFlag(Flags.ParityOverflow, before == 0x80);
            Registers.SetFlag(Flags.Subtract, true);
            Registers.SetFlag(Flags.Carry, before != 0);

            Registers.SetFlag(Flags.UndocumentedBit3, (Registers.A & 0b00001000) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (Registers.A & 0b00100000) > 0);
        }
    }
}
