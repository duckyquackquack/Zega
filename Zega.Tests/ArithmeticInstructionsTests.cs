using Moq;
using NUnit.Framework;

namespace Zega.Tests
{
    [TestFixture]
    public class ArithmeticInstructionsTests
    {
        [Test]
        [TestCase(0x87, 4)] // A + A
        [TestCase(0x80, 5)] // A + B
        [TestCase(0x81, 6)] // A + C
        [TestCase(0x82, 7)] // A + D
        [TestCase(0x83, 8)] // A + E
        [TestCase(0x84, 9)] // A + H
        [TestCase(0x85, 10)] // A + L
        public void AddsSelectedRegisterValueToRegisterA(byte opCode, byte expectedValue)
        {
            var memory = new Mock<IMemory>();
            memory.Setup(x => x.ReadByte(0x0)).Returns(opCode);

            var initialRegisterState = new Registers
            {
                A = 2,
                B = 3,
                C = 4,
                D = 5,
                E = 6,
                H = 7,
                L = 8
            };

            var z80 = new Z80(memory.Object, initialRegisterState);

            z80.Step();

            Assert.Multiple(() =>
            {
                Assert.That(z80.Registers.A, Is.EqualTo(expectedValue));
            });
        }
    }
}
