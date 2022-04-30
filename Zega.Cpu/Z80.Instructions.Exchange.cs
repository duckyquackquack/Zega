namespace Zega.Cpu
{
    public partial class Z80
    {
        private void ExchangeDEHL(byte opCode)
        {
            (Registers.DE, Registers.HL) = (Registers.HL, Registers.DE);
        }

        private void ExchangeAFShadowAF(byte opCode)
        {
            (Registers.AF, Registers.ShadowAF) = (Registers.ShadowAF, Registers.AF);
        }

        private void ExchangeX(byte opCode)
        {
            (Registers.BC, Registers.ShadowBC) = (Registers.ShadowBC, Registers.BC);
            (Registers.DE, Registers.ShadowDE) = (Registers.ShadowDE, Registers.DE);
            (Registers.HL, Registers.ShadowHL) = (Registers.ShadowHL, Registers.HL);
        }

        private void ExchangeHLWithSPDeref(byte opCode)
        {
            var loAddress = Registers.StackPointer;
            var hiAddress = (ushort)(Registers.StackPointer + 1);

            var loValue = _memory.ReadByte(loAddress);
            var hiValue = _memory.ReadByte(hiAddress);

            var hl = Registers.HL;

            _memory.WriteByte(loAddress, hl.Lo());
            _memory.WriteByte(hiAddress, hl.Hi());

            Registers.HL = hiValue.CombineWith(loValue);
        }

        private void ExchangeIndexXWithSPDeref(byte opCode)
        {
            var loAddress = Registers.StackPointer;
            var hiAddress = (ushort)(Registers.StackPointer + 1);

            var loValue = _memory.ReadByte(loAddress);
            var hiValue = _memory.ReadByte(hiAddress);

            var ix = Registers.IndexX;

            _memory.WriteByte(loAddress, ix.Lo());
            _memory.WriteByte(hiAddress, ix.Hi());

            Registers.IndexX = hiValue.CombineWith(loValue);
        }

        private void ExchangeIndexYWithSPDeref(byte opCode)
        {
            var loAddress = Registers.StackPointer;
            var hiAddress = (ushort)(Registers.StackPointer + 1);

            var loValue = _memory.ReadByte(loAddress);
            var hiValue = _memory.ReadByte(hiAddress);

            var iy = Registers.IndexY;

            _memory.WriteByte(loAddress, iy.Lo());
            _memory.WriteByte(hiAddress, iy.Hi());

            Registers.IndexY = hiValue.CombineWith(loValue);
        }
    }
}
