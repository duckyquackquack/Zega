namespace Zega.Sound
{
    internal enum LatchType
    {
        Control,
        Attenuation
    }

    /// <summary>
    /// An SN76489 sound chip
    /// </summary>
    public class SoundChip : ISoundChip
    {
        private const uint NoiseChannel = 3;
        private readonly IToneChannel[] _toneChannels;
        private readonly INoiseChannel _noiseChannel;

        private byte _latchedChannel;
        private LatchType _latchType;

        public SoundChip(IToneChannel[]? toneChannels = null, INoiseChannel? noiseChannel = null)
        {
            _latchedChannel = 0;
            _latchType = LatchType.Attenuation;

            if (toneChannels != null && toneChannels.Length != 3)
                throw new ArgumentException($"Expected only 3 tone channels, got {toneChannels.Length}");

            _toneChannels = toneChannels ?? new IToneChannel[]
            {
                new SquareWaveChannel(),
                new SquareWaveChannel(),
                new SquareWaveChannel()
            };
            _noiseChannel = noiseChannel ?? new NoiseChannel();
        }

        public void Write(byte data)
        {
            if ((data & 128) == 128) ChangeLatchAndWrite(data);
            else WriteToCurrentLatch(data);
        }

        private void ChangeLatchAndWrite(byte data)
        {
            _latchedChannel = (byte)((data & 96) >> 5);
            _latchType = (LatchType)((data & 16) >> 4);

            var dataToWrite = (byte)(data & 15);

            if (_latchedChannel < NoiseChannel)
                Write4BitsToToneChannel(dataToWrite);
            else 
                Write4BitsToNoiseChannel(dataToWrite);
        }

        private void WriteToCurrentLatch(byte data)
        {
            var dataToWrite = (byte) (data & 63);

            if (_latchedChannel < NoiseChannel)
                Write6BitsToToneChannel(dataToWrite);
            else 
                Write6BitsToNoiseChannel(dataToWrite);
        }

        private void Write4BitsToNoiseChannel(byte data)
        {
            switch (_latchType)
            {
                case LatchType.Control:
                    _noiseChannel.Control = data;
                    break;

                case LatchType.Attenuation:
                    _noiseChannel.Volume = data;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(_latchType));
            }
        }

        private void Write4BitsToToneChannel(byte data)
        {
            switch (_latchType)
            {
                case LatchType.Control:
                    _toneChannels[_latchedChannel].Frequency = data;
                    break;

                case LatchType.Attenuation:
                    _toneChannels[_latchedChannel].Volume = data;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(_latchType));
            }
        }

        private void Write6BitsToNoiseChannel(byte data)
        {
            switch (_latchType)
            {
                case LatchType.Control:
                    _noiseChannel.Control |= (ushort) ((data & 63) << 4);
                    break;

                case LatchType.Attenuation:
                    _noiseChannel.Volume = (byte) (data & 15);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(_latchType));
            }
        }

        private void Write6BitsToToneChannel(byte data)
        {
            switch (_latchType)
            {
                case LatchType.Control:
                    _toneChannels[_latchedChannel].Frequency |= (ushort) ((data & 63) << 4);
                    break;

                case LatchType.Attenuation:
                    _toneChannels[_latchedChannel].Volume = (byte) (data & 15);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(_latchType));
            }
        }
    }
}
