using NUnit.Framework;

namespace Zega.Tests
{
    [TestFixture]
    internal class RegisterTests
    {
        [Test]
        public void CombinesBAndCCorrectly()
        {
            var registers = new Registers
            {
                B = 0x31,
                C = 0x66
            };

            var bc = registers.BC;

            Assert.That(bc, Is.EqualTo(0x3166));
        }

        [Test]
        public void CombinesDAndECorrectly()
        {
            var registers = new Registers
            {
                D = 0x31,
                E = 0x66
            };

            var de = registers.DE;

            Assert.That(de, Is.EqualTo(0x3166));
        }

        [Test]
        public void CombinesHAndLCorrectly()
        {
            var registers = new Registers
            {
                H = 0x31,
                L = 0x66
            };

            var hl = registers.HL;

            Assert.That(hl, Is.EqualTo(0x3166));
        }

        [Test]
        public void CanSwapBetweenShadowRegistersAndBack()
        {
            // Set initial values
            var registers = new Registers
            {
                A = 0x1,
                B = 0x2,
                C = 0x3,
                D = 0x4,
                E = 0x5,
                F = Flags.Carry | Flags.HalfCarry,
                H = 0x6,
                L = 0x7
            };

            // Store those values into shadow registers
            registers.WriteToShadowRegisters();

            // Change all values to something different
            registers.A = 0x11;
            registers.B = 0x12;
            registers.C = 0x13;
            registers.D = 0x14;
            registers.E = 0x15;
            registers.F = Flags.ParityOverflow;
            registers.H = 0x16;
            registers.L = 0x17;

            // Restore back from shadow registers
            registers.ReadFromShadowRegisters();

            // Confirm they have been reverted back to their initial values
            Assert.Multiple(() =>
            {
                Assert.That(registers.A, Is.EqualTo(0x1));
                Assert.That(registers.B, Is.EqualTo(0x2));
                Assert.That(registers.C, Is.EqualTo(0x3));
                Assert.That(registers.D, Is.EqualTo(0x4));
                Assert.That(registers.E, Is.EqualTo(0x5));
                Assert.That(registers.F, Is.EqualTo(Flags.Carry | Flags.HalfCarry));
                Assert.That(registers.H, Is.EqualTo(0x6));
                Assert.That(registers.L, Is.EqualTo(0x7));
            });
        }
    }
}
