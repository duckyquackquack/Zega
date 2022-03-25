using System.Globalization;

namespace Zega.Tests.Fuse.OriginalFormat
{
    public class FuseExpectedCaseLoader
    {
        public static List<FuseExpectedCase> Load()
        {
            var lines = File.ReadAllLines("Fuse/OriginalFormat/Tests.Expected");

            var fuseExpectedCases = new List<FuseExpectedCase>();
            var fuseExpectedCase = new FuseExpectedCase();

            foreach (var line in lines)
            {
                var values = line.Split(' ');

                if (string.IsNullOrWhiteSpace(line))
                {
                    fuseExpectedCases.Add(fuseExpectedCase);
                    fuseExpectedCase = new FuseExpectedCase();
                }
                else if (values.Length == 1) // Test description
                {
                    fuseExpectedCase.TestDescription = line;
                }
                else if (line.StartsWith(" ")) // Event
                {
                    values = values.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    var fuseEvent = new FuseEvent();

                    fuseEvent.Time = uint.Parse(values[0]);
                    fuseEvent.EventType = GetEventTypeFromString(values[1]);
                    fuseEvent.Address = ushort.Parse(values[2], NumberStyles.HexNumber);
                    if (values.Length > 3)
                        fuseEvent.Data = byte.Parse(values[3], NumberStyles.HexNumber);

                    fuseExpectedCase.Events.Add(fuseEvent);
                }
                else if (values.Length == 12 && !values.Any(string.IsNullOrWhiteSpace) && !line.EndsWith("-1")) // 1st line of register setup
                {
                    var pos = 0;
                    fuseExpectedCase.AF = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.BC = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.DE = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.HL = ushort.Parse(values[pos++], NumberStyles.HexNumber);

                    fuseExpectedCase.ShadowAF = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.ShadowBC = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.ShadowDE = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.ShadowHL = ushort.Parse(values[pos++], NumberStyles.HexNumber);

                    fuseExpectedCase.IndexX = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.IndexY = ushort.Parse(values[pos++], NumberStyles.HexNumber);

                    fuseExpectedCase.StackPointer = ushort.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.ProgramCounter = ushort.Parse(values[pos], NumberStyles.HexNumber);
                }
                else if (values.Length == 7 && !values.Any(string.IsNullOrWhiteSpace)) // 2nd line of register setup
                {
                    var pos = 0;

                    fuseExpectedCase.InterruptVector = byte.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.MemoryRefresh = byte.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.InterruptFlipFlop1 = byte.Parse(values[pos++], NumberStyles.HexNumber) == 1;
                    fuseExpectedCase.InterruptFlipFlop2 = byte.Parse(values[pos++], NumberStyles.HexNumber) == 1;
                    fuseExpectedCase.InterruptMode = byte.Parse(values[pos++], NumberStyles.HexNumber);
                    fuseExpectedCase.Halted = byte.Parse(values[pos], NumberStyles.HexNumber) == 1;
                    fuseExpectedCase.Cycles = uint.Parse(values[^1]);
                }
                else // Expected memory block 
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

                    fuseExpectedCase.ExpectedMemoryBlocks.Add(memoryBlock);
                }
            }

            return fuseExpectedCases;
        }

        private static FuseEventType GetEventTypeFromString(string eventType)
        {
            return eventType switch
            {
                "MR" => FuseEventType.MemoryRead,
                "MW" => FuseEventType.MemoryWrite,
                "MC" => FuseEventType.MemoryContend,
                "PR" => FuseEventType.PortRead,
                "PW" => FuseEventType.PortWrite,
                "PC" => FuseEventType.PortContend,
                _ => throw new Exception($"Unrecognized event type: {eventType}")
            };
        }
    }
}
