namespace Zega
{
    [Flags]
    public enum Flags
    {
        Carry = 1,
        Subtract = 2,
        ParityOverflow = 4,
        UndocumentedBit3 = 8,
        HalfCarry = 16,
        UndocumentedBit5 = 32,
        Zero = 64,
        Sign = 128
    }
}
