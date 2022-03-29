using System.Reflection.Emit;

namespace Zega
{
    public partial class Z80
    {
        // TODO - add comments about each instruction, similar to LoadRN below

        /// <summary>
        /// LD r, n. Loads the immediate value n into register r. Register r is determined by b3-5 in the opCode
        /// </summary>
        /// <param name="opCode">The first byte read that determined this was a LoadRN instruction</param>
        private void LoadRN(byte opCode)
        {
            var register = (opCode & 56) >> 3; // 0b00111000
            var n = ReadImmediateByte();
            SetRegisterValue(register, n);
        }

        private void LoadRR(byte opCode)
        {
            var destinationRegister = (opCode & 56) >> 3;
            var sourceRegister = (opCode & 7);
            SetRegisterValue(destinationRegister, GetRegisterValue(sourceRegister));
        }

        private void LoadRHL(byte opCode)
        {
            var destinationRegister = (opCode & 56) >> 3;
            var n = _memory.ReadByte(Registers.HL);
            SetRegisterValue(destinationRegister, n);
        }

        private void LoadRIXD(byte opCode)
        {
            LoadRIndexD(Registers.IndexX, opCode);
        }

        private void LoadRIYD(byte opCode)
        {
            LoadRIndexD(Registers.IndexY, opCode);
        }

        private void LoadHLR(byte opCode)
        {
            var register = opCode & 7;
            var n = GetRegisterValue(register);
            _memory.WriteByte(Registers.HL, n);
        }

        private void LoadIXDR(byte opCode)
        {
            LoadIndexDR(Registers.IndexX, opCode);
        }

        private void LoadIYDR(byte opCode)
        {
            LoadIndexDR(Registers.IndexY, opCode);
        }

        private void LoadHLN(byte opCode)
        {
            var n = ReadImmediateByte();
            _memory.WriteByte(Registers.HL, n);
        }

        private void LoadIXDN(byte opCode)
        {
            LoadIndexDN(Registers.IndexX);
        }

        private void LoadIYDN(byte opCode)
        {
            LoadIndexDN(Registers.IndexY);
        }

        private void LoadABC(byte opCode)
        {
            LoadAFromAddress(Registers.BC);
        }

        private void LoadADE(byte opCode)
        {
            LoadAFromAddress(Registers.DE);
        }

        private void LoadANN(byte opCode)
        {
            var lo = ReadImmediateByte();
            var hi = ReadImmediateByte();
            LoadAFromAddress(hi.CombineWith(lo));
        }

        private void LoadBCA(byte opCode)
        {
            _memory.WriteByte(Registers.BC, Registers.A);
        }

        private void LoadDEA(byte opCode)
        {
            _memory.WriteByte(Registers.DE, Registers.A);
        }

        private void LoadNNA(byte opCode)
        {
            var lo = ReadImmediateByte();
            var hi = ReadImmediateByte();
            _memory.WriteByte(hi.CombineWith(lo), Registers.A);
        }

        private void LoadDDNN(byte opCode)
        {
            var lo = ReadImmediateByte();
            var hi = ReadImmediateByte();

            var registerPairCode = (opCode & 0b00110000) >> 4;

            SetRegisterPairValue(registerPairCode, hi.CombineWith(lo));
        }

        private void LoadAFromAddress(ushort address)
        {
            Registers.A = _memory.ReadByte(address);
        }

        private void LoadIndexDN(ushort index)
        {
            var d = (sbyte)ReadImmediateByte();
            var n = ReadImmediateByte();
            _memory.WriteByte((ushort)(index + d), n);
        }

        private void LoadIndexDR(ushort index, byte opCode)
        {
            var register = opCode & 7;
            var n = GetRegisterValue(register);
            var d = (sbyte)ReadImmediateByte();
            _memory.WriteByte((ushort)(index + d), n);
        }

        private void LoadRIndexD(ushort index, byte opCode)
        {
            var destinationRegister = (opCode & 56) >> 3;
            var d = (sbyte)ReadImmediateByte();
            var value = _memory.ReadByte((ushort)(index + d));
            SetRegisterValue(destinationRegister, value);
        }
    }
}
