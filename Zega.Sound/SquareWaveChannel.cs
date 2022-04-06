namespace Zega.Sound
{
    internal class SquareWaveChannel : ChannelBase, IChannel
    {
        private sbyte _polarity;

        public SquareWaveChannel()
        {
            _polarity = 1;
        }

        public void SetVolume(byte volume)
        {
            Volume = (byte)(volume & 15);
        }

        public void SetTone(int tone)
        {
            throw new NotImplementedException();
        }

        public void Tick(int cycles)
        {
            Counter -= cycles;
            if (Counter > 0) return;

            _polarity *= -1;
            Counter = Tone;
        }
    }
}
