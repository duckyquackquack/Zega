namespace Zega.Cpu
{
    public partial class Z80
    {
        /*
         * Regarding undocumented flags for this instruction group
         * From https://github.com/franckverrot/EmulationResources/blob/master/consoles/sms-gg/Z80%20Undocumented%20Features.txt
         *  LDI/LDIR/LDD/LDDR     --*0**0-  P/V set if BC not 0
                                 5 is bit 1 of (transferred byte + A)
                                 3 is bit 3 of (transferred byte + A)

            So add A to the (last) transferred byte (from (HL)), and bit 1 of that
            8 bit value is flag 5, and bit 3 is flag 3.
         */

        private void LDI(byte opCode)
        {
            var sourceValue = _memory.ReadByte(Registers.HL);
            var destinationAddress = Registers.DE;

            _memory.WriteByte(destinationAddress, sourceValue);

            Registers.DE++;
            Registers.HL++;
            Registers.BC--;

            SetBlockTransferFlags(sourceValue);
        }

        private void LDD(byte opCode)
        {
            var sourceValue = _memory.ReadByte(Registers.HL);
            var destinationAddress = Registers.DE;

            _memory.WriteByte(destinationAddress, sourceValue);

            Registers.DE--;
            Registers.HL--;
            Registers.BC--;

            SetBlockTransferFlags(sourceValue);
        }

        private void SetBlockTransferFlags(byte sourceValue)
        {
            Registers.SetFlag(Flags.HalfCarry, false);
            Registers.SetFlag(Flags.Subtract, false);
            Registers.SetFlag(Flags.ParityOverflow, Registers.BC != 0);

            Registers.SetFlag(Flags.UndocumentedBit3, ((sourceValue + Registers.A) & 0b00001000) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, ((sourceValue + Registers.A) & 0b00000010) > 0);
        }
    }
}
