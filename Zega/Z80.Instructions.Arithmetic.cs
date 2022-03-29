namespace Zega
{
    public partial class Z80
    {
        private void AddAR(byte opCode)
        {
            var register = opCode & 7; // 7 = 0b00000111
            var registerValue = GetRegisterValue(register);
            var sum = Registers.A + registerValue;

            SetAddFlags(Registers.A, registerValue, sum);

            Registers.A = (byte)sum;
        }

        private void AddAN(byte opCode)
        {
            var n = ReadImmediateByte();
            var sum = Registers.A + n;

            SetAddFlags(Registers.A, n, sum);

            Registers.A = (byte)sum;
        }

        private void AddAHL(byte opCode)
        {
            var n = _memory.ReadByte(Registers.HL);
            var sum = Registers.A + n;

            SetAddFlags(Registers.A, n, sum);

            Registers.A = (byte)sum;
        }

        private void AddAIXD(byte opCode)
        {
            var d = (sbyte)ReadImmediateByte();
            var n = _memory.ReadByte((ushort)(Registers.IndexX + d));

            var sum = Registers.A + n;

            SetAddFlags(Registers.A, n, sum);

            Registers.A = (byte)sum;
        }

        private void AddAIYD(byte opCode)
        {
            var d = (sbyte)ReadImmediateByte();
            var n = _memory.ReadByte((ushort)(Registers.IndexY + d));

            var sum = Registers.A + n;

            SetAddFlags(Registers.A, n, sum);

            Registers.A = (byte)sum;
        }

        private void AddWithCarryAR(byte opCode)
        {
            var register = opCode & 7; // 7 = 0b00000111
            var registerValue = GetRegisterValue(register);
            var sum = Registers.A + registerValue + (Registers.F.IsSet(Flags.Carry) ? 1 : 0);

            SetAddFlags(Registers.A, registerValue, sum);

            Registers.A = (byte)sum;
        }

        private void AddWithCarryAN(byte opCode)
        {
            var n = ReadImmediateByte();
            var sum = Registers.A + n + (Registers.F.IsSet(Flags.Carry) ? 1 : 0);

            SetAddFlags(Registers.A, n, sum);

            Registers.A = (byte)sum;
        }

        private void AddWithCarryAHL(byte opCode)
        {
            var n = _memory.ReadByte(Registers.HL);
            var sum = Registers.A + n + (Registers.F.IsSet(Flags.Carry) ? 1 : 0);

            SetAddFlags(Registers.A, n, sum);

            Registers.A = (byte)sum;
        }

        private void AddWithCarryAIXD(byte opCode)
        {
            var d = (sbyte)ReadImmediateByte();
            var n = _memory.ReadByte((ushort)(Registers.IndexX + d));
            var sum = Registers.A + n + (Registers.F.IsSet(Flags.Carry) ? 1 : 0);

            SetAddFlags(Registers.A, n, sum);

            Registers.A = (byte)sum;
        }

        private void AddWithCarryAIYD(byte opCode)
        {
            var d = (sbyte)ReadImmediateByte();
            var n = _memory.ReadByte((ushort)(Registers.IndexY + d));
            var sum = Registers.A + n + (Registers.F.IsSet(Flags.Carry) ? 1 : 0);

            SetAddFlags(Registers.A, n, sum);

            Registers.A = (byte)sum;
        }

        private void SubAR(byte opCode)
        {
            var register = opCode & 7; // 7 = 0b00000111
            var registerValue = GetRegisterValue(register);
            var sub = Registers.A - registerValue;

            SetSubFlags(Registers.A, registerValue, sub);

            Registers.A = (byte)sub;
        }

        private void SetSubFlags(byte a, byte b, int sub)
        {
            Registers.SetFlag(Flags.Sign, (sub & 128) > 0);
            Registers.SetFlag(Flags.Zero, sub == 0);
            Registers.SetFlag(Flags.Subtract, true);

            Registers.SetFlag(Flags.Carry, sub < 0);
            Registers.SetFlag(Flags.HalfCarry, (a & 15) < (b & 15));
            Registers.SetFlag(Flags.ParityOverflow, ((a ^ b) & 0x80) != 0 && ((b ^ sub) & 0x80) == 0);

            Registers.SetFlag(Flags.UndocumentedBit3, (sub & 8) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (sub & 32) > 0);
        }

        private void IncrementR(byte opCode)
        {
            var registerCode = (opCode & 56) >> 3;
            var registerValue = GetRegisterValue(registerCode);
            var sum = registerValue + 1;

            Registers.SetFlag(Flags.Sign, (sum & 128) > 0);
            Registers.SetFlag(Flags.Zero, (sbyte)sum == 0);
            Registers.SetFlag(Flags.Subtract, false);

            Registers.SetFlag(Flags.HalfCarry, (((registerValue & 15) + (1 & 15)) & 16) == 16);
            Registers.SetFlag(Flags.ParityOverflow, registerValue == 0x7F);

            Registers.SetFlag(Flags.UndocumentedBit3, (sum & 8) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (sum & 32) > 0);

            SetRegisterValue(registerCode, (byte)sum);
        }

        private void SetAddFlags(byte a, byte b, int sum)
        {
            Registers.SetFlag(Flags.Sign, (sum & 128) > 0);
            Registers.SetFlag(Flags.Zero, sum == 0);
            Registers.SetFlag(Flags.Subtract, false);

            Registers.SetFlag(Flags.Carry, (sum & 256) == 256);
            Registers.SetFlag(Flags.HalfCarry, (((a & 15) + (b & 15)) & 16) == 16);
            Registers.SetFlag(Flags.ParityOverflow, ((a ^ b) & 0x80) == 0 && ((Registers.A ^ sum) & 0x80) != 0);

            Registers.SetFlag(Flags.UndocumentedBit3, (sum & 8) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (sum & 32) > 0);
        }
    }
}
