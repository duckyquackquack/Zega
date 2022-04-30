namespace Zega.Cpu
{
    public abstract class BaseInstruction
    {
        protected BaseInstruction(List<int> tCycles, List<int>? branchTCycles)
        {
            TCycles = tCycles ?? throw new ArgumentNullException(nameof(tCycles));
            BranchTCycles = branchTCycles;
        }

        public List<int> TCycles { get; init; }
        public List<int>? BranchTCycles { get; init; }
    }

    public class Instruction : BaseInstruction
    {
        public Instruction(Action<byte> execute, List<int> tCycles, List<int>? branchTCycles = null)
            : base(tCycles, branchTCycles)
        {
            Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public Action<byte> Execute { get; init; }
    }

    public class DisplacementInstruction : BaseInstruction
    {
        public DisplacementInstruction(Action<byte, sbyte> execute, List<int> tCycles, List<int>? branchTCycles = null)
            : base(tCycles, branchTCycles)
        {
            Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public Action<byte, sbyte> Execute { get; init; }
    }
}
