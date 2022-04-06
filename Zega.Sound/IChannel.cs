namespace Zega.Sound;

internal interface IChannel
{
    void SetVolume(byte volume);
    void SetTone(int tone);
    void Tick(int cycles);
}