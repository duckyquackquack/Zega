using System.Collections;
using NUnit.Framework;
using Zega.Tests.Fuse.JsonFormat;

namespace Zega.Tests
{
    [TestFixture]
    public class FuseTests
    {
        [Test, TestCaseSource(nameof(GenerateFuseTestCases))]
        public void FuseTestCases(FuseTestCase testCase, FuseExpectedCase expectedCase)
        {
            var memory = CreateMemoryFromTestCase(testCase);
            var registers = CreateRegistersFromTestCase(testCase);
            
            var cpu = new Z80(memory, registers);

            uint cyclesRan = 0;
            while (cyclesRan < testCase.Cycles) // TODO check whether fuse tests are for M cycles or T cycles
                cyclesRan += cpu.Step();

            AssertFinalState(cpu, memory, expectedCase);
        }

        private static IMemory CreateMemoryFromTestCase(FuseTestCase testCase)
        {
            var memory = new FuseTestMemory();

            foreach (var memoryBlock in testCase.MemoryBlocks)
            {
                var address = memoryBlock.StartAddress;
                foreach (var datum in memoryBlock.Bytes)
                {
                    memory.WriteByte(address++, datum);
                }
            }

            return memory;
        }

        private static void AssertFinalState(Z80 cpu, IMemory memory, FuseExpectedCase expectedCase)
        {
            // TODO find a way on asserting the events
            // TODO find a way to assert on elapsed cycles

            Assert.Multiple(() =>
            {
                Assert.That(cpu.Registers.AF, Is.EqualTo(expectedCase.Af), () => "AF");
                Assert.That(cpu.Registers.BC, Is.EqualTo(expectedCase.Bc), () => "BC");
                Assert.That(cpu.Registers.DE, Is.EqualTo(expectedCase.De), () => "DE");
                Assert.That(cpu.Registers.HL, Is.EqualTo(expectedCase.Hl), () => "HL");
                Assert.That(cpu.Registers.ShadowAF, Is.EqualTo(expectedCase.ShadowAf), () => "ShadowAF");
                Assert.That(cpu.Registers.ShadowBC, Is.EqualTo(expectedCase.ShadowBc), () => "ShadowBC");
                Assert.That(cpu.Registers.ShadowDE, Is.EqualTo(expectedCase.ShadowDe), () => "ShadowDE");
                Assert.That(cpu.Registers.ShadowHL, Is.EqualTo(expectedCase.ShadowHl), () => "ShadowHL");
                Assert.That(cpu.Registers.IndexX, Is.EqualTo(expectedCase.IndexX), () => "IX");
                Assert.That(cpu.Registers.IndexY, Is.EqualTo(expectedCase.IndexY), () => "IY");
                Assert.That(cpu.Registers.StackPointer, Is.EqualTo(expectedCase.StackPointer), () => "SP");
                Assert.That(cpu.Registers.ProgramCounter, Is.EqualTo(expectedCase.ProgramCounter), () => "PC");
                Assert.That(cpu.Registers.InterruptVector, Is.EqualTo(expectedCase.InterruptVector), () => "I");

                foreach (var memoryBlock in expectedCase.ExpectedMemoryBlocks)
                {
                    var address = memoryBlock.StartAddress;
                    foreach (var datum in memoryBlock.Bytes)
                    {
                        Assert.That(memory.ReadByte(address), Is.EqualTo(datum), () => $"memory[{address}] != {datum}");
                        address++;
                    }
                }
            });
        }

        private static Registers CreateRegistersFromTestCase(FuseTestCase testCase)
        {
            return new Registers
            {
                AF = testCase.Af,
                BC = testCase.Bc,
                DE = testCase.De,
                HL = testCase.Hl,
                ShadowAF = testCase.ShadowAf,
                ShadowBC = testCase.ShadowBc,
                ShadowDE = testCase.ShadowDe,
                ShadowHL = testCase.ShadowHl,
                IndexX = testCase.IndexX,
                IndexY = testCase.IndexY,
                StackPointer = testCase.StackPointer,
                ProgramCounter = testCase.ProgramCounter,
                InterruptVector = testCase.InterruptVector,
                MemoryRefresh = testCase.MemoryRefresh
            };
        }

        public static IEnumerable GenerateFuseTestCases()
        {
            var testCases = FuseTestCase.FromJson(File.ReadAllText("Fuse/JsonFormat/In.json"));
            var expectedCases = FuseExpectedCase.FromJson(File.ReadAllText("Fuse/JsonFormat/Expected.json"));

            for (var i = 0; i < testCases.Count; i++)
            {
                var testCase = testCases[i];
                var expectedCase = expectedCases[i];

                yield return new TestCaseData(testCase, expectedCase)
                    .SetName(testCase.TestDescription);
            }
        }
    }

    internal class FuseTestMemory : IMemory
    {
        private readonly byte[] _data;

        public FuseTestMemory()
        {
            _data = new byte[64 * 1024];
        }

        public byte ReadByte(ushort address) => _data[address];
        public void WriteByte(ushort address, byte value) => _data[address] = value;
    }
}
