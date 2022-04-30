using System.Text;

namespace Zega.Cpu.Tests
{
    internal static class Z80Extensions
    {
        public static string DebugOutput(this Z80 cpu)
        {
            var cpuState = new StringBuilder();

            cpuState.AppendLine("Final CPU state:");
            cpuState.AppendLine("Decimal:");
            cpuState.AppendLine($"AF = {cpu.Registers.AF} -- A = {cpu.Registers.A} -- F = {(int)cpu.Registers.F}");
            cpuState.AppendLine($"BC = {cpu.Registers.BC} -- B = {cpu.Registers.B} -- C = {cpu.Registers.C}");
            cpuState.AppendLine($"DE = {cpu.Registers.DE} -- D = {cpu.Registers.D} -- E = {cpu.Registers.E}");
            cpuState.AppendLine($"HL = {cpu.Registers.HL} -- H = {cpu.Registers.H} -- L = {cpu.Registers.L}");
            cpuState.AppendLine($"AF' = {cpu.Registers.ShadowAF} -- A' = {cpu.Registers.ShadowA} -- F' = {(int)cpu.Registers.ShadowF}");
            cpuState.AppendLine($"BC' = {cpu.Registers.ShadowBC} -- B' = {cpu.Registers.ShadowB} -- C' = {cpu.Registers.ShadowC}");
            cpuState.AppendLine($"DE' = {cpu.Registers.ShadowDE} -- D' = {cpu.Registers.ShadowD} -- E' = {cpu.Registers.ShadowE}");
            cpuState.AppendLine($"HL' = {cpu.Registers.ShadowHL} -- H' = {cpu.Registers.ShadowH} -- L' = {cpu.Registers.ShadowL}");
            cpuState.AppendLine($"SP = {cpu.Registers.StackPointer}");
            cpuState.AppendLine($"PC = {cpu.Registers.ProgramCounter}");
            cpuState.AppendLine($"IX = {cpu.Registers.IndexX}");
            cpuState.AppendLine($"IY = {cpu.Registers.IndexY}");

            cpuState.AppendLine("\nHex:");
            cpuState.AppendLine($"AF = 0x{cpu.Registers.AF:X} -- A = 0x{cpu.Registers.A:X} -- F = 0x{cpu.Registers.F:X}");
            cpuState.AppendLine($"BC = 0x{cpu.Registers.BC:X} -- B = 0x{cpu.Registers.B:X} -- C = 0x{cpu.Registers.C:X}");
            cpuState.AppendLine($"DE = 0x{cpu.Registers.DE:X} -- D = 0x{cpu.Registers.D:X} -- E = 0x{cpu.Registers.E:X}");
            cpuState.AppendLine($"HL = 0x{cpu.Registers.HL:X} -- H = 0x{cpu.Registers.H:X} -- L = 0x{cpu.Registers.L:X}");
            cpuState.AppendLine($"AF' = 0x{cpu.Registers.ShadowAF:X} -- A' = 0x{cpu.Registers.ShadowA:X} -- F' = 0x{cpu.Registers.ShadowF:X}");
            cpuState.AppendLine($"BC' = 0x{cpu.Registers.ShadowBC:X} -- B' = 0x{cpu.Registers.ShadowB:X} -- C' = 0x{cpu.Registers.ShadowC:X}");
            cpuState.AppendLine($"DE' = 0x{cpu.Registers.ShadowDE:X} -- D' = 0x{cpu.Registers.ShadowD:X} -- E' = 0x{cpu.Registers.ShadowE:X}");
            cpuState.AppendLine($"HL' = 0x{cpu.Registers.ShadowHL:X} -- H' = 0x{cpu.Registers.ShadowH:X} -- L' = 0x{cpu.Registers.ShadowL:X}");
            cpuState.AppendLine($"SP = 0x{cpu.Registers.StackPointer:X}");
            cpuState.AppendLine($"PC = 0x{cpu.Registers.ProgramCounter:X}");
            cpuState.AppendLine($"IX = 0x{cpu.Registers.IndexX:X}");
            cpuState.AppendLine($"IY = 0x{cpu.Registers.IndexY:X}");

            cpuState.AppendLine("\nBinary:");
            cpuState.AppendLine($"AF = 0b{Convert.ToString(cpu.Registers.AF, 2).PadLeft(16, '0')} -- A = 0b{Convert.ToString(cpu.Registers.A, 2).PadLeft(8, '0')} -- F = 0b{Convert.ToString((int)cpu.Registers.F, 2).PadLeft(8, '0')}");
            cpuState.AppendLine($"BC = 0b{Convert.ToString(cpu.Registers.BC, 2).PadLeft(16, '0')} -- B = 0b{Convert.ToString(cpu.Registers.B, 2).PadLeft(8, '0')} -- C = 0b{Convert.ToString(cpu.Registers.C, 2).PadLeft(8, '0')}");
            cpuState.AppendLine($"DE = 0b{Convert.ToString(cpu.Registers.DE, 2).PadLeft(16, '0')} -- D = 0b{Convert.ToString(cpu.Registers.D, 2).PadLeft(8, '0')} -- E = 0b{Convert.ToString(cpu.Registers.E, 2).PadLeft(8, '0')}");
            cpuState.AppendLine($"HL = 0b{Convert.ToString(cpu.Registers.HL, 2).PadLeft(16, '0')} -- H = 0b{Convert.ToString(cpu.Registers.H, 2).PadLeft(8, '0')} -- L = 0b{Convert.ToString(cpu.Registers.L, 2).PadLeft(8, '0')}");
            cpuState.AppendLine($"AF' = 0b{Convert.ToString(cpu.Registers.ShadowAF, 2).PadLeft(16, '0')} -- A' = 0b{Convert.ToString(cpu.Registers.ShadowA, 2).PadLeft(8, '0')} -- F' = 0b{Convert.ToString((int)cpu.Registers.ShadowF, 2).PadLeft(8, '0')}");
            cpuState.AppendLine($"BC' = 0b{Convert.ToString(cpu.Registers.ShadowBC, 2).PadLeft(16, '0')} -- B' = 0b{Convert.ToString(cpu.Registers.ShadowB, 2).PadLeft(8, '0')} -- C' = 0b{Convert.ToString(cpu.Registers.ShadowC, 2).PadLeft(8, '0')}");
            cpuState.AppendLine($"DE' = 0b{Convert.ToString(cpu.Registers.ShadowDE, 2).PadLeft(16, '0')} -- D' = 0b{Convert.ToString(cpu.Registers.ShadowD, 2).PadLeft(8, '0')} -- E' = 0b{Convert.ToString(cpu.Registers.ShadowE, 2).PadLeft(8, '0')}");
            cpuState.AppendLine($"HL' = 0b{Convert.ToString(cpu.Registers.ShadowHL, 2).PadLeft(16, '0')} -- H' = 0b{Convert.ToString(cpu.Registers.ShadowH, 2).PadLeft(8, '0')} -- L' = 0b{Convert.ToString(cpu.Registers.ShadowL, 2).PadLeft(8, '0')}");
            cpuState.AppendLine($"SP = 0b{Convert.ToString(cpu.Registers.StackPointer, 2).PadLeft(16, '0')}");
            cpuState.AppendLine($"PC = 0b{Convert.ToString(cpu.Registers.ProgramCounter, 2).PadLeft(16, '0')}");
            cpuState.AppendLine($"IX = 0b{Convert.ToString(cpu.Registers.IndexX, 2).PadLeft(16, '0')}");
            cpuState.AppendLine($"IY = 0b{Convert.ToString(cpu.Registers.IndexY, 2).PadLeft(16, '0')}");

            return cpuState.ToString();
        }
    }
}
