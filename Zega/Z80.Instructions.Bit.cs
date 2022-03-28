namespace Zega
{
    public partial class Z80
    {
        public void BitBR(byte opCode)
        {
            var bitToTest = (opCode & 0b00111000) >> 3;
            var registerCode = opCode & 7;

            var registerValue = GetRegisterValue(registerCode);
            var bitValue = registerValue & (1 << bitToTest);

            Registers.SetFlag(Flags.Subtract, false);
            Registers.SetFlag(Flags.ParityOverflow, bitValue == 0);
            Registers.SetFlag(Flags.HalfCarry, true);
            Registers.SetFlag(Flags.Zero, bitValue == 0);
            Registers.SetFlag(Flags.Sign, bitToTest == 7 && bitValue > 0);

            Registers.SetFlag(Flags.UndocumentedBit3, (registerValue & 8) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (registerValue & 32) > 0);
        }

        public void SetBR(byte opCode)
        {
            var bitToSet = (opCode & 0b00111000) >> 3;
            var registerCode = opCode & 7;
            var registerValue = GetRegisterValue(registerCode);

            SetRegisterValue(registerCode, (byte)(registerValue | (1 << bitToSet)));
        }

        public void ResBR(byte opCode)
        {
            var bitToReset = (opCode & 0b00111000) >> 3;
            var registerCode = opCode & 7;
            var registerValue = GetRegisterValue(registerCode);

            SetRegisterValue(registerCode, (byte)(registerValue & ~(1 << bitToReset)));
        }
    }
}
