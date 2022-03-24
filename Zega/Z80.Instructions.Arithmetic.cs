namespace Zega
{
    public partial class Z80
    {
        public void AddAR(byte opCode)
        {
            var register = opCode & 0b00000111;
            var n = GetRegisterValue(register);
            Registers.A = (byte) ((Registers.A + n) & 0xFF);
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
