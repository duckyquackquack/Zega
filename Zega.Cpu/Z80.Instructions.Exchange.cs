namespace Zega.Cpu
{
    public partial class Z80
    {
        private void ExchangeDEHL(byte opCode)
        {
            (Registers.DE, Registers.HL) = (Registers.HL, Registers.DE);
        }

        private void ExchangeAFShadowAF(byte opCode)
        {
            (Registers.AF, Registers.ShadowAF) = (Registers.ShadowAF, Registers.AF);
        }

        private void ExchangeX(byte opCode)
        {
            (Registers.BC, Registers.ShadowBC) = (Registers.ShadowBC, Registers.BC);
            (Registers.DE, Registers.ShadowDE) = (Registers.ShadowDE, Registers.DE);
            (Registers.HL, Registers.ShadowHL) = (Registers.ShadowHL, Registers.HL);
        }
    }
}
