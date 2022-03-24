namespace Zega
{
    public partial class Z80
    {
        private void None(byte opCode)
        { }

        private void Implied(byte opCode)
        { }

        private void Immediate(byte opCode)
        {
            _nextMemoryLocation = Registers.ProgramCounter++;
        }

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
