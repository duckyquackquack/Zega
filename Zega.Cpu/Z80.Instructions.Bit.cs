namespace Zega.Cpu
{
    public partial class Z80
    {
        private void BitBR(byte opCode)
        {
            var bitToTest = (opCode & 0b00111000) >> 3;
            var registerCode = opCode & 7;

            var registerValue = GetRegisterValue(registerCode);
            var bitValue = registerValue & (1 << bitToTest);

            SetBitGroupFlags(bitValue, bitToTest, registerValue);
        }

        private void BitBHL(byte opCode)
        {
            var bitToTest = (opCode & 0b00111000) >> 3;
            var valueToTest = _memory.ReadByte(Registers.HL);
            var bitValue = valueToTest & (1 << bitToTest);

            SetBitGroupFlags(bitValue, bitToTest, valueToTest);
        }

        private void SetBR(byte opCode)
        {
            var bitToSet = (opCode & 0b00111000) >> 3;
            var registerCode = opCode & 7;
            var registerValue = GetRegisterValue(registerCode);

            SetRegisterValue(registerCode, (byte)(registerValue | (1 << bitToSet)));
        }

        private void SetBHL(byte opCode)
        {
            var bitToSet = (opCode & 0b00111000) >> 3;
            var valueToTest = _memory.ReadByte(Registers.HL);

            _memory.WriteByte(Registers.HL, (byte)(valueToTest | (1 << bitToSet)));
        }

        private void ResBR(byte opCode)
        {
            var bitToReset = (opCode & 0b00111000) >> 3;
            var registerCode = opCode & 7;
            var registerValue = GetRegisterValue(registerCode);

            SetRegisterValue(registerCode, (byte)(registerValue & ~(1 << bitToReset)));
        }

        private void ResBHL(byte opCode)
        {
            var bitToReset = (opCode & 0b00111000) >> 3;
            var valueToTest = _memory.ReadByte(Registers.HL);

            _memory.WriteByte(Registers.HL, (byte)(valueToTest & ~(1 << bitToReset)));
        }

        private void SetBitGroupFlags(int bitValue, int bitToTest, byte valueToTest)
        {
            Registers.SetFlag(Flags.Subtract, false);
            Registers.SetFlag(Flags.ParityOverflow, bitValue == 0);
            Registers.SetFlag(Flags.HalfCarry, true);
            Registers.SetFlag(Flags.Zero, bitValue == 0);
            Registers.SetFlag(Flags.Sign, bitToTest == 7 && bitValue > 0);

            Registers.SetFlag(Flags.UndocumentedBit3, (valueToTest & 8) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (valueToTest & 32) > 0);
        }
    }
}
