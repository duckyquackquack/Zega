namespace Zega.Sound
{
    public static class Volume
    {
        public static uint[] Levels { get; }

        static Volume()
        {
            // 0x0 means full volume and 0xF means silence
            // each volume step is 2 decibels quieter (80%) than the last

            const byte volumeSteps = 16;
            const uint maxVolume = 8000;
            const float volumeReductionFactor = 0.8f;

            Levels = new uint[volumeSteps];
            Levels[0] = maxVolume;

            for (var i = 1; i < volumeSteps; i++)
            {
                Levels[i] = (uint)(Levels[i - 1] * volumeReductionFactor);
            }

            Levels[^1] = 0;
        }
    }
}
