namespace Zega.Cpu
{
    public partial class Z80
    {
        private void RotateLeftCarryA(byte opCode)
        {
            var shiftedA = Registers.A << 1;
            Registers.A = (byte) shiftedA;
            var carry = (shiftedA & 0b100000000) > 0;

            if (carry)
                SetBR(0b00000111);
            else 
                ResBR(0b00000111);

            SetRotateLeftFlags(carry);
        }

        private void RotateLeftA(byte opCode)
        {
            var oldCarry = Registers.F.IsSet(Flags.Carry);
            var newCarry = (Registers.A & 0b10000000) > 0;
            Registers.A <<= 1;
            
            if (oldCarry)
                SetBR(0b00000111);
            else
                ResBR(0b00000111);

            SetRotateLeftFlags(newCarry);
        }

        private void SetRotateLeftFlags(bool carry)
        {
            Registers.SetFlag(Flags.HalfCarry, false);
            Registers.SetFlag(Flags.Subtract, false);
            Registers.SetFlag(Flags.Carry, carry);

            Registers.SetFlag(Flags.UndocumentedBit3, (Registers.A & 8) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (Registers.A & 32) > 0);
        }
    }
}
