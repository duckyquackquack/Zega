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
            LoadRIndexD(Registers.IndexX, opCode);
        }

        public void LoadRIYD(byte opCode)
        {
            LoadRIndexD(Registers.IndexY, opCode);
        }

        public void LoadHLR(byte opCode)
        {
            var register = opCode & 7;
            var n = GetRegisterValue(register);
            _memory.WriteByte(Registers.HL, n);
        }

        public void LoadIXDR(byte opCode)
        {
            LoadIndexDR(Registers.IndexX, opCode);
        }

        public void LoadIYDR(byte opCode)
        {
            LoadIndexDR(Registers.IndexY, opCode);
        }

        public void LoadHLN(byte opCode)
        {
            var n = ReadImmediateByte();
            _memory.WriteByte(Registers.HL, n);
        }

        public void LoadIXDN(byte opCode)
        {
            LoadIndexDN(Registers.IndexX);
        }

        public void LoadIYDN(byte opCode)
        {
            LoadIndexDN(Registers.IndexY);
        }

        public void LoadABC(byte opCode)
        {
            LoadAFromAddress(Registers.BC);
        }

        public void LoadADE(byte opCode)
        {
            LoadAFromAddress(Registers.DE);
        }

        public void LoadANN(byte opCode)
        {
            var lo = ReadImmediateByte();
            var hi = ReadImmediateByte();
            LoadAFromAddress(hi.CombineWith(lo));
        }

        public void LoadBCA(byte opCode)
        {
            _memory.WriteByte(Registers.BC, Registers.A);
        }

        public void LoadDEA(byte opCode)
        {
            _memory.WriteByte(Registers.DE, Registers.A);
        }

        public void LoadNNA(byte opCode)
        {
            var lo = ReadImmediateByte();
            var hi = ReadImmediateByte();
            _memory.WriteByte(hi.CombineWith(lo), Registers.A);
        }

        public void LoadDDNN(byte opCode)
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
