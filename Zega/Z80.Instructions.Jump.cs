namespace Zega
{
    public partial class Z80
    {
        public void JumpNN(byte opCode)
        {
            var lo = ReadImmediateByte();
            var hi = ReadImmediateByte();

            Registers.ProgramCounter = hi.CombineWith(lo);
        }

        public void JumpCCNN(byte opCode)
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
