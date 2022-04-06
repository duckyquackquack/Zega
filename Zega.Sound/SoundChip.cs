namespace Zega.Sound
{
    internal enum LatchType
    {
        Tone,
        Volume
    }

    internal class SoundChip
    {
        private static readonly uint[] Volumes;

        static SoundChip()
        {
            // 0x0 means full volume and 0xF means silence
            // each volume step is 2 decibels quieter (80%) than the last

            const byte volumeSteps = 16;
            const uint maxVolume = 8000;
            const float volumeReductionFactor = 0.8f;

            Volumes = new uint[volumeSteps];
            Volumes[0] = maxVolume;

            for (var i = 1; i < volumeSteps; i++)
            {
                Volumes[i] = (uint)(Volumes[i - 1] * volumeReductionFactor);
            }

            Volumes[^1] = 0;
        }

        private readonly IChannel[] _channels;
        private byte _latchedChannel;
        private LatchType _latchType;

        public SoundChip()
        {
            _latchedChannel = 0;
            _latchType = LatchType.Volume;

            _channels = new IChannel[]
            {
                new SquareWaveChannel(),
                new SquareWaveChannel(),
                new SquareWaveChannel(),
                new NoiseChannel()
            };
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

            WriteToCurrentLatch((byte)(data & 15));
        }

        private void WriteToCurrentLatch(byte data)
        {
            switch (_latchType)
            {
                case LatchType.Tone:
                    _channels[_latchedChannel].SetTone(data);
                    break;

                case LatchType.Volume:
                    _channels[_latchedChannel].SetVolume((byte) (data & 15));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(_latchType));
            }
        }
    }
}
