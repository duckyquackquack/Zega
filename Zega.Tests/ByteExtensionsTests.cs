using NUnit.Framework;

namespace Zega.Tests
{
    [TestFixture]
    internal class ByteExtensionsTests
    {
        [Test]
        public void CombinesTwoBytesToUShort()
        {
            const byte hi = 0x12;
            const byte lo = 0x34;
            const ushort expectedAnswer = 0x1234;

            var answer = hi.CombineWith(lo);

            Assert.That(answer, Is.EqualTo(expectedAnswer));
        }
    }
}
