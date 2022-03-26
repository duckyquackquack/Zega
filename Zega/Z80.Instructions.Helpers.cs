namespace Zega
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

        private byte ReadImmediateByte()
        {
            return _memory.ReadByte(Registers.ProgramCounter++);
        }
    }
}
