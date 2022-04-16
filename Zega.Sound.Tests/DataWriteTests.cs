using FluentAssertions;
using NUnit.Framework;

namespace Zega.Sound.Tests
{
    [TestFixture]
    internal class DataWriteTests
    {
        private IToneChannel[] _toneChannels = null!;
        private INoiseChannel _noiseChannel = null!;
        private ISoundChip _soundChip = null!;

        [SetUp]
        public void Setup()
        {
            _toneChannels = new IToneChannel[]
            {
                new SquareWaveChannel(),
                new SquareWaveChannel(),
                new SquareWaveChannel()
            };
            _noiseChannel = new NoiseChannel();
            _soundChip = new SoundChip(_toneChannels, _noiseChannel);
        }

        [Test]
        public void SetChannel0FrequencyTo440Hz()
        {
            // Arrange
            const byte instruction1 = 0b10001110; // Latch to channel 0 tone register, data = 1110
            const byte instruction2 = 0b00001111; // Data = 001111

            const ushort expectedResult = 0b0011111110; // == 0xFE == 440 dec

            // Act
            _soundChip.Write(instruction1);
            _soundChip.Write(instruction2);

            // Assert
            _toneChannels[0].Frequency.Should().Be(expectedResult,
                "because the two instructions are expected to change to channel 0's frequency register and provide 0xFE as data");
        }

        [Test]
        public void SetChannel1VolumeToLowest()
        {
            // Arrange
            const byte instruction = 0b10111111; // Latch to channel 1 volume register, data = 1111
            const byte expectedResult = 0b00001111;

            // Act
            _soundChip.Write(instruction);

            // Assert
            _toneChannels[1].Volume.Should().Be(expectedResult, "because we muted channel 1's volume register");
        }

        [Test]
        public void DataByteIsNotIgnored()
        {
            // Arrange
            const byte instruction1 = 0b11011111; // Latch to channel 2 volume register, data = 1111
            const byte instruction2 = 0b00000000; // Write 0000 to current latch (only 4 bits because latch type is volume)
            const byte expectedResult = 0b00000000;

            // Act
            _soundChip.Write(instruction1);
            _soundChip.Write(instruction2);

            // Assert
            _toneChannels[2].Volume.Should().Be(expectedResult, "because we muted channel 2's volume register followed immediately by a max volume instruction");
        }

        [Test]
        public void SetNoiseChannelToMediumShiftWhiteNoise()
        {
            // Arrange
            const byte instruction = 0b11100101; // Latch to channel 3 control register, data = 0101
            const ushort expectedResult = 0b00000101;

            // Act
            _soundChip.Write(instruction);

            // Assert
            _noiseChannel.Control.Should().Be(expectedResult,
                "because we latched the noise channel (3)'s control register and passed in 5 as data");
        }

        [Test]
        public void DataByteNotIgnoredForNoiseChannel()
        {
            // Arrange
            const byte instruction1 = 0b11100101; // Latch to channel 3 control register, data = 0101
            const byte instruction2 = 0b00000100; // Write 000100 to current latch (all 6 bits because it's the control register)
            const byte expectedResult = 0b00000100;

            // Act
            _soundChip.Write(instruction1);
            _soundChip.Write(instruction2);

            // Assert
            _noiseChannel.Control.Should().Be(expectedResult,
                "because we latched the noise channel, passed in 5 as data and then passed in 4 as the next data byte");
        }
    }
}