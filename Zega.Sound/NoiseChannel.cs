namespace Zega.Sound
{
    internal class NoiseChannel : ChannelBase, IChannel
    {
        private readonly INoiseGenerator _whiteNoiseGenerator;
        private readonly INoiseGenerator _periodicNoiseGenerator;

        public NoiseChannel()
        {
            _whiteNoiseGenerator = new WhiteNoiseGenerator();
            _periodicNoiseGenerator = new PeriodicNoiseGenerator();
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
            throw new NotImplementedException();
        }
    }
}
