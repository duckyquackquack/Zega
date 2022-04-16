namespace Zega.Sound
{
    public class NoiseChannel : INoiseChannel
    {
        private readonly INoiseGenerator _whiteNoiseGenerator;
        private readonly INoiseGenerator _periodicNoiseGenerator;

        public NoiseChannel()
        {
            _whiteNoiseGenerator = new WhiteNoiseGenerator();
            _periodicNoiseGenerator = new PeriodicNoiseGenerator();
        }

        public byte Volume { get; set; }
        public ushort Control { get; set; }

        public void Tick(int cycles)
        {
            throw new NotImplementedException();
        }
    }
}
