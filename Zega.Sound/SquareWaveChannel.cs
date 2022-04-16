namespace Zega.Sound
{
    public class SquareWaveChannel : IToneChannel
    {
        private int _polarity;
        private int _counter;

        public byte Volume { get; set; }
        public ushort Frequency { get; set; }
        
        public SquareWaveChannel()
        {
            _polarity = 1;
            _counter = 0;

            Volume = 0;
            Frequency = 0;
        }

        public void Tick(int cycles)
        {
            _counter -= cycles;
            if (_counter > 0) return;

            _polarity *= -1;
            _counter = Frequency;
        }
    }
}
