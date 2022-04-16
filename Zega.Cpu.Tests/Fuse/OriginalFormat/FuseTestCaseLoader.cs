using System.Globalization;

namespace Zega.Cpu.Tests.Fuse.OriginalFormat
{
    public class FuseTestCaseLoader
    {
        public static List<FuseTestCase> Load()
        {
            var lines = File.ReadAllLines("Fuse/OriginalFormat/Tests.In");

            var fuseTestCases = new List<FuseTestCase>();
            var fuseTestCase = new FuseTestCase();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var values = line.Split(' ');

                if (line == "-1")
                {
                    fuseTestCases.Add(fuseTestCase);
                    fuseTestCase = new FuseTestCase();
                }
                else if (values.Length == 1) // Test description
                {
                    fuseTestCase.TestDescription = line;
                }
                else if (values.Length == 12 && !values.Any(string.IsNullOrWhiteSpace)) // 1st line of register setup
                {
                    var pos = 0;
                    fuseTestCase.AF = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.BC = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.DE = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.HL = ushort.Parse(values[pos++], NumberStyles.HexNumber);

                    fuseTestCase.ShadowAF = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.ShadowBC = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.ShadowDE = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.ShadowHL = ushort.Parse(values[pos++], NumberStyles.HexNumber);

                    fuseTestCase.IndexX = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.IndexY = ushort.Parse(values[pos++], NumberStyles.HexNumber);

                    fuseTestCase.StackPointer = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.ProgramCounter = ushort.Parse(values[pos], NumberStyles.HexNumber);
                }
                else if (line.EndsWith("-1")) // Memory block
                {
                    var startAddress = ushort.Parse(values[0], NumberStyles.HexNumber);

                    var bytes = new List<byte>();

                    for (var i = 1; i < values.Length - 1; i++)
                        bytes.Add(byte.Parse(values[i], NumberStyles.HexNumber));

                    var memoryBlock = new TestCaseMemoryBlock
                    {
                        Bytes = bytes,
                        StartAddress = startAddress
                    };
                    
                    fuseTestCase.MemoryBlocks.Add(memoryBlock);
                }
                else // 2nd line of register setup
                {
                    var pos = 0;

                    fuseTestCase.InterruptVector = byte.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.MemoryRefresh = byte.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.InterruptFlipFlop1 = byte.Parse(values[pos++], NumberStyles.HexNumber) == 1;
                    fuseTestCase.InterruptFlipFlop2 = byte.Parse(values[pos++], NumberStyles.HexNumber) == 1;
                    fuseTestCase.InterruptMode = byte.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseTestCase.Halted = byte.Parse(values[pos], NumberStyles.HexNumber) == 1;
                    fuseTestCase.Cycles = uint.Parse(values[^1]);
                }
            }

            return fuseTestCases;
        }
    }
}
