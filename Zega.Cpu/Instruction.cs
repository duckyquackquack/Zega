namespace Zega.Cpu
{
    public class Instruction
    {
        public Instruction(Action<byte> execute, List<int> tCycles, List<int>? branchTCycles = null)
        {
            Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            TCycles = tCycles ?? throw new ArgumentNullException(nameof(tCycles));
            BranchTCycles = branchTCycles;
        }

        public Action<byte> Execute { get; init; }
        public List<int> TCycles { get; init; }
        public List<int>? BranchTCycles { get; init; }
    }
}
