namespace Zega.Cpu
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
            LoadAFromAddress(ReadImmediateUshort());
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
            _memory.WriteByte(ReadImmediateUshort(), Registers.A);
        }

        private void LoadDDNN(byte opCode)
        {
            var registerPairCode = (opCode & 0b00110000) >> 4;
            SetRegisterPairValue(registerPairCode, ReadImmediateUshort());
        }

        private void LoadDDFromAddressNN(byte opCode)
        {
            var registerPairCode = (opCode & 0b00110000) >> 4;
            SetRegisterPairValue(registerPairCode, ReadUshortFromImmediateAddress());
        }

        private void LoadIndexXFromAddressNN(byte opCode)
        {
            Registers.IndexX = ReadUshortFromImmediateAddress();
        }

        private void LoadIndexYFromAddressNN(byte opCode)
        {
            Registers.IndexY = ReadUshortFromImmediateAddress();
        }

        private void LoadIndexXNN(byte opCode)
        {
            Registers.IndexX = ReadImmediateUshort();
        }

        private void LoadIndexYNN(byte opCode)
        {
            Registers.IndexY = ReadImmediateUshort();
        }

        private void LoadAFromAddress(ushort address)
        {
            Registers.A = _memory.ReadByte(address);
        }

        private void LoadHLFromAddressNN(byte opCode)
        {
            Registers.HL = ReadUshortFromImmediateAddress();
        }

        private void LoadNNHL(byte opCode)
        {
            var address = ReadImmediateUshort();
            _memory.WriteByte(address, Registers.L);
            _memory.WriteByte((ushort)(address + 1), Registers.H);
        }

        private void LoadAddressNNFromDD(byte opCode)
        {
            var registerPairCode = (opCode & 0b00110000) >> 4;
            var registerPairValue = GetRegisterPairValue(registerPairCode);

            var address = ReadImmediateUshort();
            _memory.WriteByte(address, registerPairValue.Lo());
            _memory.WriteByte((ushort)(address + 1), registerPairValue.Hi());
        }

        private void LoadAddressNNFromIndexX(byte opCode)
        {
            var address = ReadImmediateUshort();
            _memory.WriteByte(address, Registers.IndexX.Lo());
            _memory.WriteByte((ushort)(address + 1), Registers.IndexX.Hi());
        }

        private void LoadAddressNNFromIndexY(byte opCode)
        {
            var address = ReadImmediateUshort();
            _memory.WriteByte(address, Registers.IndexY.Lo());
            _memory.WriteByte((ushort)(address + 1), Registers.IndexY.Hi());
        }

        private void LoadSPFromHL(byte opCode)
        {
            Registers.StackPointer = Registers.HL;
        }

        private void LoadSPFromIndexX(byte opCode)
        {
            Registers.StackPointer = Registers.IndexX;
        }

        private void LoadSPFromIndexY(byte opCode)
        {
            Registers.StackPointer = Registers.IndexY;
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

        private void PushRegisterPair(byte opCode)
        {
            var registerPairCode = (opCode & 0b00110000) >> 4;
            var registerPairValue = GetRegisterPairValueForPush(registerPairCode);

            _memory.WriteByte(--Registers.StackPointer, registerPairValue.Hi());
            _memory.WriteByte(--Registers.StackPointer, registerPairValue.Lo());
        }

        private void PushIndexX(byte opCode)
        {
            _memory.WriteByte(--Registers.StackPointer, Registers.IndexX.Hi());
            _memory.WriteByte(--Registers.StackPointer, Registers.IndexX.Lo());
        }

        private void PushIndexY(byte opCode)
        {
            _memory.WriteByte(--Registers.StackPointer, Registers.IndexY.Hi());
            _memory.WriteByte(--Registers.StackPointer, Registers.IndexY.Lo());
        }

        private void PopRegisterPair(byte opCode)
        {
            var registerPairCode = (opCode & 0b00110000) >> 4;

            var lo = _memory.ReadByte(Registers.StackPointer++);
            var hi = _memory.ReadByte(Registers.StackPointer++);

            SetRegisterPairValueForPop(registerPairCode, hi.CombineWith(lo));
        }

        private void PopIndexX(byte opCode)
        {
            var lo = _memory.ReadByte(Registers.StackPointer++);
            var hi = _memory.ReadByte(Registers.StackPointer++);

            Registers.IndexX = hi.CombineWith(lo);
        }

        private void PopIndexY(byte opCode)
        {
            var lo = _memory.ReadByte(Registers.StackPointer++);
            var hi = _memory.ReadByte(Registers.StackPointer++);

            Registers.IndexY = hi.CombineWith(lo);
        }
    }
}
