namespace Zega
{
    public partial class Z80
    {
        // TODO - see if we can actually live without these. for the time being I can't see them adding much help

        private void None(byte opCode)
        { }

        private void Implied(byte opCode)
        { }

        private void Immediate(byte opCode)
        { }

        private void ExtendedImmediate(byte opCode)
        { }

        private void Register(byte opCode)
        { }

        private void RegisterIndirect(byte opCode)
        { }

        private void Extended(byte opCode)
        { }

        private void ModifiedPageZero(byte opCode)
        { }

        private void Relative(byte opCode)
        { }

        private void Indexed(byte opCode)
        { }

        private void Bit(byte opCode)
        { }
    }
}
