namespace Zega
{
    public class Instruction
    {
        public Instruction(/*Action<byte> addressingMode1, Action<byte> addressingMode2, */Action<byte> execute, List<int> cycles)
        {
            Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            Cycles = cycles ?? throw new ArgumentNullException(nameof(cycles));
            
            //AddressingMode1 = addressingMode1;
            //AddressingMode2 = addressingMode2;
        }

        //public Action<byte> AddressingMode1 { get; init; }
        //public Action<byte> AddressingMode2 { get; init; }
        public Action<byte> Execute { get; init; }
        public List<int> Cycles { get; init; }
    }
}
