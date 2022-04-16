namespace Zega.Sound;

public interface INoiseChannel
{
    byte Volume { get; set; }
    ushort Control { get; set; }
    void Tick(int cycles);
}