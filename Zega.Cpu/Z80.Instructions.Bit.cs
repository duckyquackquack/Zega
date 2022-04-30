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

        private void BitBIndexXD(byte opCode, sbyte displacement)
        {
            var bit = (opCode & 0b00111000) >> 3;
            var address = (ushort)(Registers.IndexX + displacement);
            var result = _memory.ReadByte(address) & (1 << bit);

            SetDisplacementBitTestFlags(result, bit, address);
        }

        private void BitBIndexYD(byte opCode, sbyte displacement)
        {
            var bit = (opCode & 0b00111000) >> 3;
            var address = (ushort)(Registers.IndexY + displacement);
            var result = _memory.ReadByte(address) & (1 << bit);

            SetDisplacementBitTestFlags(result, bit, address);
        }

        private void SetDisplacementBitTestFlags(int result, int bit, ushort address)
        {
            Registers.SetFlag(Flags.Zero, result == 0);
            Registers.SetFlag(Flags.HalfCarry, true);
            Registers.SetFlag(Flags.Subtract, false);

            // Undocumented behaviour of documented flags
            Registers.SetFlag(Flags.Sign, bit == 7 && result != 0);
            Registers.SetFlag(Flags.ParityOverflow, result == 0);

            // Undocumented behaviour of undocumented flags
            Registers.SetFlag(Flags.UndocumentedBit5, (address.Hi() & 32) > 0);
            Registers.SetFlag(Flags.UndocumentedBit3, (address.Hi() & 8) > 0);

            // Console.WriteLine($"Result = {result} dec | 0x{result:X} hex | 0b{Convert.ToString(result, 2).PadLeft(8, '0')} bin");
            // Console.WriteLine($"Bit = {bit} dec | 0x{bit:X} hex | 0b{Convert.ToString(bit, 2).PadLeft(8, '0')} bin");
            // Console.WriteLine($"Address = {address} dec | 0x{address:X} hex | 0b{Convert.ToString(address, 2).PadLeft(16, '0')} bin");
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
