namespace Zega.Sound
{
    public interface IToneChannel
    {
        byte Volume { get; set; }
        ushort Frequency { get; set; }
        void Tick(int cycles);
    }
}
