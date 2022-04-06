namespace Zega.Cpu
{
    public static class FlagsExtensions
    {
        public static bool IsSet(this Flags f, Flags flagToCheck)
        {
            return ((int) f & (int) flagToCheck) > 0;
        }

        public static void Set(this ref Flags f, Flags flagToSet)
        {
            f |= flagToSet;
        }

        public static void Reset(this ref Flags f, Flags flagToReset)
        {
            f &= ~flagToReset;
        }
    }
}
