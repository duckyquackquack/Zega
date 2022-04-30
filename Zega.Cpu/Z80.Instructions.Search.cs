namespace Zega.Cpu
{
    public partial class Z80
    {
        /*
         * Regarding undocumented flags.
         * From https://github.com/franckverrot/EmulationResources/blob/master/consoles/sms-gg/Z80%20Undocumented%20Features.txt
         *  CPI/CPIR/CPD/CPDR     SZ*H**1-  P/V set if BC not 0
                                S,Z,H from (A - (HL) ) as in CP (HL)
                                3 is bit 3 of (A - (HL) - H)
                                5 is bit 1 of (A - (HL) - H)

            CPI instructions are weird too. The test is simply like a CP (HL). Flag 3 and
            5 are set like this: Take A, subtract the last (HL), and then decrease it
            with 1 if the H flag was set (/after/ the CP). Bit 1 of this value is flag
            5, bit 3 is flag 3.
         */

        private void CPI(byte opCode)
        {
            var value = _memory.ReadByte(Registers.HL);
            var subtraction = Registers.A - value;

            Registers.HL++;
            Registers.BC--;

            SetSearchFlags(subtraction, value);
        }

        private void CPD(byte opCode)
        {
            var value = _memory.ReadByte(Registers.HL);
            var subtraction = Registers.A - value;

            Registers.HL--;
            Registers.BC--;

            SetSearchFlags(subtraction, value);
        }

        private void SetSearchFlags(int subtraction, byte value)
        {
            Registers.SetFlag(Flags.Zero, subtraction == 0);
            Registers.SetFlag(Flags.Subtract, true);
            Registers.SetFlag(Flags.Sign, subtraction < 0);
            Registers.SetFlag(Flags.ParityOverflow, Registers.BC != 0);
            Registers.SetFlag(Flags.HalfCarry, (Registers.A & 0x0F) < (value & 0x0F));

            var undocumentedSubtraction = subtraction - ((Registers.F & Flags.HalfCarry) != 0 ? 1 : 0);

            Registers.SetFlag(Flags.UndocumentedBit3, (undocumentedSubtraction & 0b00001000) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (undocumentedSubtraction & 0b00000010) > 0);
        }
    }
}
