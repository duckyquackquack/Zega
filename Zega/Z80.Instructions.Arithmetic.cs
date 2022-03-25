namespace Zega
{
    public partial class Z80
    {
        public void AddAR(byte opCode)
        {
            var register = opCode & 0b00000111;
            var n = GetRegisterValue(register);
            var sum = (Registers.A + n);

            SetArithmeticGroupFlags(sum);
            Registers.A = (byte) sum;
        }

        private void SetArithmeticGroupFlags(int result)
        {
            Registers.SetFlag(Flags.Sign, (result & 0b10000000) > 0);
            Registers.SetFlag(Flags.Zero, result == 0);
            Registers.SetFlag(Flags.Subtract, false);

            Registers.SetFlag(Flags.Carry, result > 255);
            Registers.SetFlag(Flags.HalfCarry, true);
            Registers.SetFlag(Flags.ParityOverflow, true);

            Registers.SetFlag(Flags.UndocumentedBit3, (result & 0b00001000) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (result & 0b00100000) > 0);
        }

        private byte GetRegisterValue(int registerCode)
        {
            return registerCode switch
            {
                0b00000111 => Registers.A,
                0b00000000 => Registers.B,
                0b00000001 => Registers.C,
                0b00000010 => Registers.D,
                0b00000011 => Registers.E,
                0b00000100 => Registers.H,
                0b00000101 => Registers.L,
                _ => throw new NotSupportedException($"Unrecognized register code: 0x{registerCode:X}")
            };
        }
    }
}
