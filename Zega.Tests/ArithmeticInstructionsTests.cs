using System.Collections;
using Moq;
using NUnit.Framework;
using Zega.Cpu;

namespace Zega.Tests
{
    // TODO, with the Fuse tests testing the f out of all instructions, is it really worth writing our own instruction tests?
    [TestFixture]
    public class ArithmeticInstructionsTests
    {
        [Test]
        [TestCaseSource(nameof(AddARTestCases))]
        public void AddsSelectedRegisterValueToRegisterA(byte opCode, byte a, byte registerValue, byte expectedValue)
        {
            var memory = new Mock<IMemory>();
            memory.Setup(x => x.ReadByte(0x0)).Returns(opCode);

            var initialRegisterState = new Registers
            {
                A = a,
                B = registerValue,
                C = registerValue,
                D = registerValue,
                E = registerValue,
                H = registerValue,
                L = registerValue
            };

            var z80 = new Z80(memory.Object, initialRegisterState);

            z80.Step();

            Assert.That(z80.Registers.A, Is.EqualTo(expectedValue));
        }

        // TODO add a test that tests each flag being set or not.
        // e.g. a test for when we expect Sign to be set and also one when it shouldn't be set and so on

        private static IEnumerable AddARTestCases()
        {
            var opCodes = new byte[] { 0x80, 0x81, 0x82, 0x83, 0x84, 0x85 };
            var registerNames = new[] { "B", "C", "D", "E", "H", "L" };

            // As much as I would love to test *all* possible values, it generates nearly 460,000 test cases
            var registerValues = new byte[] {0, 1, 2, 4, 8, 16, 32, 64, 128, 255};

            // Adding other registers to A
            for (var opCode = 0; opCode < opCodes.Length; opCode++)
            {
                for (byte a = 0; a < registerValues.Length; a++)
                {
                    for (byte registerValue = 0; registerValue < registerValues.Length; registerValue++)
                    {
                        yield return new TestCaseData(opCodes[opCode], a, registerValue, (byte)(a + registerValue))
                            .SetName($"ADD A,r where A = 0x{registerValues[a]:X}, r = {registerNames[opCode]}, {registerNames[opCode]} = 0x{registerValues[registerValue]:X}");
                    }
                }
            }

            // Adding A to itself
            for (byte registerValue = 0; registerValue < registerValues.Length; registerValue++)
            {
                yield return new TestCaseData((byte) 0x87, registerValue, registerValue, (byte)(registerValue + registerValue))
                    .SetName($"ADD A,A where A = 0x{registerValues[registerValue]:X}");
            }
        }
    }
}
