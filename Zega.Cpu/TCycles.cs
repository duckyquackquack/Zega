namespace Zega.Cpu
{
    public static class TCycles
    {
        public static List<int> T4 { get; } = new() { 4 };
        public static List<int> T43 { get; } = new() { 4, 3 };
        public static List<int> T44 { get; } = new() { 4, 4 };
        public static List<int> T433 { get; } = new() { 4, 3, 3 };
        public static List<int> T444 { get; } = new() { 4, 4, 4 };
        public static List<int> T4333 { get; } = new() { 4, 3, 3, 3 };
        public static List<int> T4443 { get; } = new() { 4, 4, 4, 3 };
        public static List<int> T44353 { get; } = new() { 4, 4, 3, 5, 3 };
    }
}
