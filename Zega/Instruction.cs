namespace Zega
{
    public class Instruction
    {
        public Instruction(/*Action<byte> addressingMode1, Action<byte> addressingMode2, */Action<byte> execute, uint mCycles, List<int> tCycles)
        {
            Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            TCycles = tCycles ?? throw new ArgumentNullException(nameof(tCycles));
            MCycles = mCycles;
            //AddressingMode1 = addressingMode1;
            //AddressingMode2 = addressingMode2;
        }

        //public Action<byte> AddressingMode1 { get; init; }
        //public Action<byte> AddressingMode2 { get; init; }
        public Action<byte> Execute { get; init; }
        public List<int> TCycles { get; init; }
        public uint MCycles { get; set; }
    }
}
