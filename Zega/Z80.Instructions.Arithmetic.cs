namespace Zega
{
    public partial class Z80
    {
        public void AddAR(byte opCode)
        {
            var register = opCode & 7; // 7 = 0b00000111
            var n = GetRegisterValue(register);
            var sum = Registers.A + n;

            SetArithmeticGroupFlags(Registers.A, n, sum);
            Registers.A = (byte) sum;
        }

        // TODO - if firstOperand always turns out to be register A, then just use it directly rather than having another param
        private void SetArithmeticGroupFlags(byte firstOperand, byte secondOperand, int result)
        {
            Registers.SetFlag(Flags.Sign, (result & 128) > 0);
            Registers.SetFlag(Flags.Zero, result == 0);
            Registers.SetFlag(Flags.Subtract, false);

            Registers.SetFlag(Flags.Carry, (result & 256) == 256);
            Registers.SetFlag(Flags.HalfCarry, (((firstOperand & 15) + (secondOperand & 15)) & 16) == 16);
            Registers.SetFlag(Flags.ParityOverflow, true); // TODO set this correctly

            Registers.SetFlag(Flags.UndocumentedBit3, (result & 8) > 0);
            Registers.SetFlag(Flags.UndocumentedBit5, (result & 32) > 0);
        }
    }
}
