using System.Reflection.Emit;

namespace Zega
{
    public partial class Z80
    {
        public void LoadRN(byte opCode)
        {
            var register = (opCode & 56) >> 3; // 0b00111000
            var n = ReadImmediateByte();
            SetRegisterValue(register, n);
        }

        public void LoadRR(byte opCode)
        {
            var destinationRegister = (opCode & 56) >> 3;
            var sourceRegister = (opCode & 7);
            SetRegisterValue(destinationRegister, GetRegisterValue(sourceRegister));
        }

        public void LoadRHL(byte opCode)
        {
            var destinationRegister = (opCode & 56) >> 3;
            var n = _memory.ReadByte(Registers.HL);
            SetRegisterValue(destinationRegister, n);
        }

        public void LoadRIXD(byte opCode)
        {
            var destinationRegister = (ReadImmediateByte() & 56) >> 3;
            var d = (sbyte) ReadImmediateByte();
            var value = _memory.ReadByte((ushort) (Registers.IndexX + d));
            SetRegisterValue(destinationRegister, value);
        }
    }
}
