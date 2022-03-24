using NUnit.Framework;

namespace Zega.Tests
{
    [TestFixture]
    internal class FlagsExtensionsTests
    {
        [Test]
        [TestCase(Flags.HalfCarry, Flags.HalfCarry)]
        [TestCase(Flags.ParityOverflow, Flags.ParityOverflow)]
        [TestCase(Flags.Carry, Flags.Carry)]
        [TestCase(Flags.Sign, Flags.Sign)]
        [TestCase(Flags.Subtract, Flags.Subtract)]
        [TestCase(Flags.Zero, Flags.Zero)]
        [TestCase(Flags.UndocumentedBit3, Flags.UndocumentedBit3)]
        [TestCase(Flags.UndocumentedBit5, Flags.UndocumentedBit5)]
        [TestCase(Flags.HalfCarry | Flags.Carry, Flags.Carry)]
        [TestCase(Flags.HalfCarry | Flags.Carry, Flags.HalfCarry)]
        [TestCase((Flags)255, Flags.HalfCarry)]
        public void ReturnsIsSetWhenFlagIsSet(Flags f, Flags flagToCheck)
        {
            Assert.True(f.IsSet(flagToCheck));
        }

        [Test]
        [TestCase(Flags.HalfCarry, Flags.Carry)]
        [TestCase(Flags.ParityOverflow, Flags.Carry)]
        [TestCase(Flags.Carry, Flags.Sign)]
        [TestCase(Flags.Sign, Flags.Carry)]
        [TestCase(Flags.Subtract, Flags.Carry)]
        [TestCase(Flags.Zero, Flags.Carry)]
        [TestCase(Flags.UndocumentedBit3, Flags.Carry)]
        [TestCase(Flags.UndocumentedBit5, Flags.Sign)]
        [TestCase(Flags.HalfCarry | Flags.Carry, Flags.Sign)]
        [TestCase(Flags.HalfCarry | Flags.Carry, Flags.Subtract)]
        [TestCase((Flags)0, Flags.HalfCarry)]
        public void ReturnsIsNotSetWhenFlagIsNotSet(Flags f, Flags flagToCheck)
        {
            Assert.False(f.IsSet(flagToCheck));
        }

        [Test]
        public void SetsFlagWhenRequested()
        {
            var f = Flags.Carry;

            f.Set(Flags.HalfCarry);

            Assert.Multiple(() =>
            {
                Assert.True(f.IsSet(Flags.Carry));
                Assert.True(f.IsSet(Flags.HalfCarry));

                Assert.False(f.IsSet(Flags.UndocumentedBit3));
                Assert.False(f.IsSet(Flags.UndocumentedBit5));
                Assert.False(f.IsSet(Flags.Sign));
                Assert.False(f.IsSet(Flags.Subtract));
                Assert.False(f.IsSet(Flags.Zero));
                Assert.False(f.IsSet(Flags.ParityOverflow));
            });
        }

        [Test]
        public void SetsMultipleFlagsWhenRequested()
        {
            var f = Flags.Carry;

            f.Set(Flags.HalfCarry | Flags.Sign);

            Assert.Multiple(() =>
            {
                Assert.True(f.IsSet(Flags.Carry));
                Assert.True(f.IsSet(Flags.HalfCarry));
                Assert.True(f.IsSet(Flags.Sign));

                Assert.False(f.IsSet(Flags.UndocumentedBit3));
                Assert.False(f.IsSet(Flags.UndocumentedBit5));
                Assert.False(f.IsSet(Flags.Subtract));
                Assert.False(f.IsSet(Flags.Zero));
                Assert.False(f.IsSet(Flags.ParityOverflow));
            });
        }

        [Test]
        public void ResetsFlagWhenRequested()
        {
            var f = Flags.Carry | Flags.HalfCarry;

            f.Reset(Flags.HalfCarry);

            Assert.Multiple(() =>
            {
                Assert.True(f.IsSet(Flags.Carry));

                Assert.False(f.IsSet(Flags.HalfCarry));
                Assert.False(f.IsSet(Flags.UndocumentedBit3));
                Assert.False(f.IsSet(Flags.UndocumentedBit5));
                Assert.False(f.IsSet(Flags.Sign));
                Assert.False(f.IsSet(Flags.Subtract));
                Assert.False(f.IsSet(Flags.Zero));
                Assert.False(f.IsSet(Flags.ParityOverflow));
            });
        }

        [Test]
        public void ResetsMultipleFlagsWhenRequested()
        {
            var f = Flags.Carry | Flags.HalfCarry | Flags.Subtract;

            f.Reset(Flags.HalfCarry | Flags.Carry);

            Assert.Multiple(() =>
            {
                Assert.True(f.IsSet(Flags.Subtract));

                Assert.False(f.IsSet(Flags.Carry));
                Assert.False(f.IsSet(Flags.HalfCarry));
                Assert.False(f.IsSet(Flags.UndocumentedBit3));
                Assert.False(f.IsSet(Flags.UndocumentedBit5));
                Assert.False(f.IsSet(Flags.Sign));
                Assert.False(f.IsSet(Flags.Zero));
                Assert.False(f.IsSet(Flags.ParityOverflow));
            });
        }
    }
}
