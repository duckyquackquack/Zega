namespace Zega
{
    public partial class Z80
    {
        public void AddAR(byte opCode)
        {
            var register = opCode & 7; // 7 = 0b00000111
            var n = GetRegisterValue(register);
            var sum = Registers.A + n;

            Registers.SetFlag(Flags.Sign, (sum & 128) > 0);
            Registers.SetFlag(Flags.Zero, sum == 0);
            Registers.SetFlag(Flags.Subtract, false);

            Registers.SetFlag(Flags.Carry, (sum & 256) == 256);
            Registers.SetFlag(Flags.HalfCarry, (((Registers.A & 15) + (n & 15)) & 16) == 16);
            Registers.SetFlag(Flags.ParityOverflow, true); // TODO set this correctly

            Registers.SetFlag(Flags.UndocumentedBit3, (sum & 8) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (sum & 32) > 0);

            Registers.A = (byte) sum;
        }

        public void IncrementR(byte opCode)
        {
            var registerCode = (opCode & 56) >> 3;
            var registerValue = GetRegisterValue(registerCode);
            var sum = registerValue + 1;

            Registers.SetFlag(Flags.Sign, (sum & 128) > 0);
            Registers.SetFlag(Flags.Zero, (sbyte) sum == 0);
            Registers.SetFlag(Flags.Subtract, false);

            Registers.SetFlag(Flags.HalfCarry, (((registerValue & 15) + (1 & 15)) & 16) == 16);
            Registers.SetFlag(Flags.ParityOverflow, registerValue == 0x7F);

            Registers.SetFlag(Flags.UndocumentedBit3, (sum & 8) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (sum & 32) > 0);

            SetRegisterValue(registerCode, (byte) sum);
        }
    }
}
