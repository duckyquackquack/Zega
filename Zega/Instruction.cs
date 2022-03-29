namespace Zega
{
    public class Instruction
    {
        public Instruction(Action<byte> execute, List<int> tCycles)
        {
            Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            TCycles = tCycles ?? throw new ArgumentNullException(nameof(tCycles));
        }

        public Action<byte> Execute { get; init; }
        public List<int> TCycles { get; init; }
    }
}
