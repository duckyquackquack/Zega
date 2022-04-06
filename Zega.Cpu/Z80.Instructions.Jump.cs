namespace Zega.Cpu
{
    public partial class Z80
    {
        private void JumpNN(byte opCode)
        {
            var lo = ReadImmediateByte();
            var hi = ReadImmediateByte();

            Registers.ProgramCounter = hi.CombineWith(lo);
        }

        private void JumpCCNN(byte opCode)
        {
            var flagCode = (opCode & 56) >> 3;
            var isSet = GetFlagStatusFromFlagCode(flagCode);

            var lo = ReadImmediateByte();
            var hi = ReadImmediateByte();

            if (isSet)
                Registers.ProgramCounter = hi.CombineWith(lo);
        }
    }
}
