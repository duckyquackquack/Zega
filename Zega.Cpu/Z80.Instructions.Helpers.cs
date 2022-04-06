namespace Zega.Cpu
{
    public partial class Z80
    {
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

        private void SetRegisterValue(int registerCode, byte value)
        {
            switch (registerCode)
            {
                case 0b00000111: Registers.A = value; break;
                case 0b00000000: Registers.B = value; break;
                case 0b00000001: Registers.C = value; break;
                case 0b00000010: Registers.D = value; break;
                case 0b00000011: Registers.E = value; break;
                case 0b00000100: Registers.H = value; break;
                case 0b00000101: Registers.L = value; break;
                default: throw new NotSupportedException($"Unrecognized register code: 0x{registerCode:X}");
            }
        }

        private void SetRegisterPairValue(int registerCode, ushort value)
        {
            switch (registerCode)
            {
                case 0b00: Registers.BC = value; break;
                case 0b01: Registers.DE = value; break;
                case 0b10: Registers.HL = value; break;
                case 0b11: Registers.StackPointer = value; break;
                default: throw new NotSupportedException($"Unrecognized register code: 0x{registerCode:X}");
            }
        }

        private bool GetFlagStatusFromFlagCode(int flagCode)
        {
            return flagCode switch
            {
                0b00000000 => !Registers.F.IsSet(Flags.Zero),
                0b00000001 => Registers.F.IsSet(Flags.Zero),
                0b00000010 => !Registers.F.IsSet(Flags.Carry),
                0b00000011 => Registers.F.IsSet(Flags.Carry),
                0b00000100 => !Registers.F.IsSet(Flags.ParityOverflow),
                0b00000101 => Registers.F.IsSet(Flags.ParityOverflow),
                0b00000110 => !Registers.F.IsSet(Flags.Sign),
                0b00000111 => Registers.F.IsSet(Flags.Sign),
                _ => throw new NotSupportedException($"Unrecognized flag code: 0x{flagCode:X}")
            };
        }

        private byte ReadImmediateByte()
        {
            return _memory.ReadByte(Registers.ProgramCounter++);
        }
    }
}
