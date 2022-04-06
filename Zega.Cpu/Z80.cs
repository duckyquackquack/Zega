﻿namespace Zega.Cpu
{
    public partial class Z80
    {
        // TODO add halted bool
        // TODO add IFF1 and IFF2
        // TODO add interrupt mode enum
        // TODO - should I refactor all instruction method names to be longer but more descriptive?
        // e.g. RLCA -> RotateLeftCarryA

        // TODO - see if we can make this private again. I don't like how it's public just so classes outside of Z80.cs,
        // namely the unit tests, can set and get them freely for initial setup and post-result Asserts
        public readonly Registers Registers;
        private readonly IMemory _memory;

        private readonly IList<IInstructionGroup> _prefixedInstructionGroups;
        private readonly IInstructionGroup _nonPrefixedInstructionGroup;

        public Z80(IMemory memory, Registers? registers = null)
        {
            _memory = memory ?? throw new ArgumentNullException(nameof(memory));
            Registers = registers ?? new Registers();

            _prefixedInstructionGroups = new List<IInstructionGroup>
            {
                new CBInstructionGroup(new Dictionary<byte, Instruction>
                {
                    { 0x40, new Instruction(BitBR, TCycles.T44) },
                    { 0x41, new Instruction(BitBR, TCycles.T44) },
                    { 0x42, new Instruction(BitBR, TCycles.T44) },
                    { 0x43, new Instruction(BitBR, TCycles.T44) },
                    { 0x44, new Instruction(BitBR, TCycles.T44) },
                    { 0x45, new Instruction(BitBR, TCycles.T44) },
                    { 0x46, new Instruction(BitBHL, TCycles.T444) },
                    { 0x47, new Instruction(BitBR, TCycles.T44) },
                    { 0x48, new Instruction(BitBR, TCycles.T44) },
                    { 0x49, new Instruction(BitBR, TCycles.T44) },
                    { 0x4A, new Instruction(BitBR, TCycles.T44) },
                    { 0x4B, new Instruction(BitBR, TCycles.T44) },
                    { 0x4C, new Instruction(BitBR, TCycles.T44) },
                    { 0x4D, new Instruction(BitBR, TCycles.T44) },
                    { 0x4E, new Instruction(BitBHL, TCycles.T444) },
                    { 0x4F, new Instruction(BitBR, TCycles.T44) },
                    { 0x50, new Instruction(BitBR, TCycles.T44) },
                    { 0x51, new Instruction(BitBR, TCycles.T44) },
                    { 0x52, new Instruction(BitBR, TCycles.T44) },
                    { 0x53, new Instruction(BitBR, TCycles.T44) },
                    { 0x54, new Instruction(BitBR, TCycles.T44) },
                    { 0x55, new Instruction(BitBR, TCycles.T44) },
                    { 0x56, new Instruction(BitBHL, TCycles.T444) },
                    { 0x57, new Instruction(BitBR, TCycles.T44) },
                    { 0x58, new Instruction(BitBR, TCycles.T44) },
                    { 0x59, new Instruction(BitBR, TCycles.T44) },
                    { 0x5A, new Instruction(BitBR, TCycles.T44) },
                    { 0x5B, new Instruction(BitBR, TCycles.T44) },
                    { 0x5C, new Instruction(BitBR, TCycles.T44) },
                    { 0x5D, new Instruction(BitBR, TCycles.T44) },
                    { 0x5E, new Instruction(BitBHL, TCycles.T444) },
                    { 0x5F, new Instruction(BitBR, TCycles.T44) },
                    { 0x60, new Instruction(BitBR, TCycles.T44) },
                    { 0x61, new Instruction(BitBR, TCycles.T44) },
                    { 0x62, new Instruction(BitBR, TCycles.T44) },
                    { 0x63, new Instruction(BitBR, TCycles.T44) },
                    { 0x64, new Instruction(BitBR, TCycles.T44) },
                    { 0x65, new Instruction(BitBR, TCycles.T44) },
                    { 0x66, new Instruction(BitBHL, TCycles.T444) },
                    { 0x67, new Instruction(BitBR, TCycles.T44) },
                    { 0x68, new Instruction(BitBR, TCycles.T44) },
                    { 0x69, new Instruction(BitBR, TCycles.T44) },
                    { 0x6A, new Instruction(BitBR, TCycles.T44) },
                    { 0x6B, new Instruction(BitBR, TCycles.T44) },
                    { 0x6C, new Instruction(BitBR, TCycles.T44) },
                    { 0x6D, new Instruction(BitBR, TCycles.T44) },
                    { 0x6E, new Instruction(BitBHL, TCycles.T444) },
                    { 0x6F, new Instruction(BitBR, TCycles.T44) },
                    { 0x70, new Instruction(BitBR, TCycles.T44) },
                    { 0x71, new Instruction(BitBR, TCycles.T44) },
                    { 0x72, new Instruction(BitBR, TCycles.T44) },
                    { 0x73, new Instruction(BitBR, TCycles.T44) },
                    { 0x74, new Instruction(BitBR, TCycles.T44) },
                    { 0x75, new Instruction(BitBR, TCycles.T44) },
                    { 0x76, new Instruction(BitBHL, TCycles.T444) },
                    { 0x77, new Instruction(BitBR, TCycles.T44) },
                    { 0x78, new Instruction(BitBR, TCycles.T44) },
                    { 0x79, new Instruction(BitBR, TCycles.T44) },
                    { 0x7A, new Instruction(BitBR, TCycles.T44) },
                    { 0x7B, new Instruction(BitBR, TCycles.T44) },
                    { 0x7C, new Instruction(BitBR, TCycles.T44) },
                    { 0x7D, new Instruction(BitBR, TCycles.T44) },
                    { 0x7E, new Instruction(BitBHL, TCycles.T444) },
                    { 0x7F, new Instruction(BitBR, TCycles.T44) },
                    { 0x80, new Instruction(ResBR, TCycles.T44) },
                    { 0x81, new Instruction(ResBR, TCycles.T44) },
                    { 0x82, new Instruction(ResBR, TCycles.T44) },
                    { 0x83, new Instruction(ResBR, TCycles.T44) },
                    { 0x84, new Instruction(ResBR, TCycles.T44) },
                    { 0x85, new Instruction(ResBR, TCycles.T44) },
                    { 0x86, new Instruction(ResBHL, TCycles.T4443) },
                    { 0x87, new Instruction(ResBR, TCycles.T44) },
                    { 0x88, new Instruction(ResBR, TCycles.T44) },
                    { 0x89, new Instruction(ResBR, TCycles.T44) },
                    { 0x8A, new Instruction(ResBR, TCycles.T44) },
                    { 0x8B, new Instruction(ResBR, TCycles.T44) },
                    { 0x8C, new Instruction(ResBR, TCycles.T44) },
                    { 0x8D, new Instruction(ResBR, TCycles.T44) },
                    { 0x8E, new Instruction(ResBHL, TCycles.T4443) },
                    { 0x8F, new Instruction(ResBR, TCycles.T44) },
                    { 0x90, new Instruction(ResBR, TCycles.T44) },
                    { 0x91, new Instruction(ResBR, TCycles.T44) },
                    { 0x92, new Instruction(ResBR, TCycles.T44) },
                    { 0x93, new Instruction(ResBR, TCycles.T44) },
                    { 0x94, new Instruction(ResBR, TCycles.T44) },
                    { 0x95, new Instruction(ResBR, TCycles.T44) },
                    { 0x96, new Instruction(ResBHL, TCycles.T4443) },
                    { 0x97, new Instruction(ResBR, TCycles.T44) },
                    { 0x98, new Instruction(ResBR, TCycles.T44) },
                    { 0x99, new Instruction(ResBR, TCycles.T44) },
                    { 0x9A, new Instruction(ResBR, TCycles.T44) },
                    { 0x9B, new Instruction(ResBR, TCycles.T44) },
                    { 0x9C, new Instruction(ResBR, TCycles.T44) },
                    { 0x9D, new Instruction(ResBR, TCycles.T44) },
                    { 0x9E, new Instruction(ResBHL, TCycles.T4443) },
                    { 0x9F, new Instruction(ResBR, TCycles.T44) },
                    { 0xA0, new Instruction(ResBR, TCycles.T44) },
                    { 0xA1, new Instruction(ResBR, TCycles.T44) },
                    { 0xA2, new Instruction(ResBR, TCycles.T44) },
                    { 0xA3, new Instruction(ResBR, TCycles.T44) },
                    { 0xA4, new Instruction(ResBR, TCycles.T44) },
                    { 0xA5, new Instruction(ResBR, TCycles.T44) },
                    { 0xA6, new Instruction(ResBHL, TCycles.T4443) },
                    { 0xA7, new Instruction(ResBR, TCycles.T44) },
                    { 0xA8, new Instruction(ResBR, TCycles.T44) },
                    { 0xA9, new Instruction(ResBR, TCycles.T44) },
                    { 0xAA, new Instruction(ResBR, TCycles.T44) },
                    { 0xAB, new Instruction(ResBR, TCycles.T44) },
                    { 0xAC, new Instruction(ResBR, TCycles.T44) },
                    { 0xAD, new Instruction(ResBR, TCycles.T44) },
                    { 0xAE, new Instruction(ResBHL, TCycles.T4443) },
                    { 0xAF, new Instruction(ResBR, TCycles.T44) },
                    { 0xB0, new Instruction(ResBR, TCycles.T44) },
                    { 0xB1, new Instruction(ResBR, TCycles.T44) },
                    { 0xB2, new Instruction(ResBR, TCycles.T44) },
                    { 0xB3, new Instruction(ResBR, TCycles.T44) },
                    { 0xB4, new Instruction(ResBR, TCycles.T44) },
                    { 0xB5, new Instruction(ResBR, TCycles.T44) },
                    { 0xB6, new Instruction(ResBHL, TCycles.T4443) },
                    { 0xB7, new Instruction(ResBR, TCycles.T44) },
                    { 0xB8, new Instruction(ResBR, TCycles.T44) },
                    { 0xB9, new Instruction(ResBR, TCycles.T44) },
                    { 0xBA, new Instruction(ResBR, TCycles.T44) },
                    { 0xBB, new Instruction(ResBR, TCycles.T44) },
                    { 0xBC, new Instruction(ResBR, TCycles.T44) },
                    { 0xBD, new Instruction(ResBR, TCycles.T44) },
                    { 0xBE, new Instruction(ResBHL, TCycles.T4443) },
                    { 0xBF, new Instruction(ResBR, TCycles.T44) },
                    { 0xC0, new Instruction(SetBR, TCycles.T44) },
                    { 0xC1, new Instruction(SetBR, TCycles.T44) },
                    { 0xC2, new Instruction(SetBR, TCycles.T44) },
                    { 0xC3, new Instruction(SetBR, TCycles.T44) },
                    { 0xC4, new Instruction(SetBR, TCycles.T44) },
                    { 0xC5, new Instruction(SetBR, TCycles.T44) },
                    { 0xC6, new Instruction(SetBHL, TCycles.T4443) },
                    { 0xC7, new Instruction(SetBR, TCycles.T44) },
                    { 0xC8, new Instruction(SetBR, TCycles.T44) },
                    { 0xC9, new Instruction(SetBR, TCycles.T44) },
                    { 0xCA, new Instruction(SetBR, TCycles.T44) },
                    { 0xCB, new Instruction(SetBR, TCycles.T44) },
                    { 0xCC, new Instruction(SetBR, TCycles.T44) },
                    { 0xCD, new Instruction(SetBR, TCycles.T44) },
                    { 0xCE, new Instruction(SetBHL, TCycles.T4443) },
                    { 0xCF, new Instruction(SetBR, TCycles.T44) },
                    { 0xD0, new Instruction(SetBR, TCycles.T44) },
                    { 0xD1, new Instruction(SetBR, TCycles.T44) },
                    { 0xD2, new Instruction(SetBR, TCycles.T44) },
                    { 0xD3, new Instruction(SetBR, TCycles.T44) },
                    { 0xD4, new Instruction(SetBR, TCycles.T44) },
                    { 0xD5, new Instruction(SetBR, TCycles.T44) },
                    { 0xD6, new Instruction(SetBHL, TCycles.T4443) },
                    { 0xD7, new Instruction(SetBR, TCycles.T44) },
                    { 0xD8, new Instruction(SetBR, TCycles.T44) },
                    { 0xD9, new Instruction(SetBR, TCycles.T44) },
                    { 0xDA, new Instruction(SetBR, TCycles.T44) },
                    { 0xDB, new Instruction(SetBR, TCycles.T44) },
                    { 0xDC, new Instruction(SetBR, TCycles.T44) },
                    { 0xDD, new Instruction(SetBR, TCycles.T44) },
                    { 0xDE, new Instruction(SetBHL, TCycles.T4443) },
                    { 0xDF, new Instruction(SetBR, TCycles.T44) },
                    { 0xE0, new Instruction(SetBR, TCycles.T44) },
                    { 0xE1, new Instruction(SetBR, TCycles.T44) },
                    { 0xE2, new Instruction(SetBR, TCycles.T44) },
                    { 0xE3, new Instruction(SetBR, TCycles.T44) },
                    { 0xE4, new Instruction(SetBR, TCycles.T44) },
                    { 0xE5, new Instruction(SetBR, TCycles.T44) },
                    { 0xE6, new Instruction(SetBHL, TCycles.T4443) },
                    { 0xE7, new Instruction(SetBR, TCycles.T44) },
                    { 0xE8, new Instruction(SetBR, TCycles.T44) },
                    { 0xE9, new Instruction(SetBR, TCycles.T44) },
                    { 0xEA, new Instruction(SetBR, TCycles.T44) },
                    { 0xEB, new Instruction(SetBR, TCycles.T44) },
                    { 0xEC, new Instruction(SetBR, TCycles.T44) },
                    { 0xED, new Instruction(SetBR, TCycles.T44) },
                    { 0xEE, new Instruction(SetBHL, TCycles.T4443) },
                    { 0xEF, new Instruction(SetBR, TCycles.T44) },
                    { 0xF0, new Instruction(SetBR, TCycles.T44) },
                    { 0xF1, new Instruction(SetBR, TCycles.T44) },
                    { 0xF2, new Instruction(SetBR, TCycles.T44) },
                    { 0xF3, new Instruction(SetBR, TCycles.T44) },
                    { 0xF4, new Instruction(SetBR, TCycles.T44) },
                    { 0xF5, new Instruction(SetBR, TCycles.T44) },
                    { 0xF6, new Instruction(SetBHL, TCycles.T4443) },
                    { 0xF7, new Instruction(SetBR, TCycles.T44) },
                    { 0xF8, new Instruction(SetBR, TCycles.T44) },
                    { 0xF9, new Instruction(SetBR, TCycles.T44) },
                    { 0xFA, new Instruction(SetBR, TCycles.T44) },
                    { 0xFB, new Instruction(SetBR, TCycles.T44) },
                    { 0xFC, new Instruction(SetBR, TCycles.T44) },
                    { 0xFD, new Instruction(SetBR, TCycles.T44) },
                    { 0xFE, new Instruction(SetBHL, TCycles.T4443) },
                    { 0xFF, new Instruction(SetBR, TCycles.T44) }
                }),

                new DDInstructionGroup(new Dictionary<byte, Instruction>
                {
                    { 0x36, new Instruction(LoadIXDN, TCycles.T44353) },
                    { 0x46, new Instruction(LoadRIXD, TCycles.T44353) },
                    { 0x4E, new Instruction(LoadRIXD, TCycles.T44353) },
                    { 0x56, new Instruction(LoadRIXD, TCycles.T44353) },
                    { 0x5E, new Instruction(LoadRIXD, TCycles.T44353) },
                    { 0x66, new Instruction(LoadRIXD, TCycles.T44353) },
                    { 0x6E, new Instruction(LoadRIXD, TCycles.T44353) },
                    { 0x70, new Instruction(LoadIXDR, TCycles.T44353) },
                    { 0x71, new Instruction(LoadIXDR, TCycles.T44353) },
                    { 0x72, new Instruction(LoadIXDR, TCycles.T44353) },
                    { 0x73, new Instruction(LoadIXDR, TCycles.T44353) },
                    { 0x74, new Instruction(LoadIXDR, TCycles.T44353) },
                    { 0x75, new Instruction(LoadIXDR, TCycles.T44353) },
                    { 0x77, new Instruction(LoadIXDR, TCycles.T44353) },
                    { 0x7E, new Instruction(LoadRIXD, TCycles.T44353) },
                    { 0x86, new Instruction(AddAIXD, TCycles.T44353) },
                    { 0x8E, new Instruction(AddWithCarryAIXD, TCycles.T44353) }
                }),
                new FDInstructionGroup(new Dictionary<byte, Instruction>
                {
                    { 0x36, new Instruction(LoadIYDN, TCycles.T44353) },
                    { 0x46, new Instruction(LoadRIYD, TCycles.T44353) },
                    { 0x4E, new Instruction(LoadRIYD, TCycles.T44353) },
                    { 0x56, new Instruction(LoadRIYD, TCycles.T44353) },
                    { 0x5E, new Instruction(LoadRIYD, TCycles.T44353) },
                    { 0x66, new Instruction(LoadRIYD, TCycles.T44353) },
                    { 0x6E, new Instruction(LoadRIYD, TCycles.T44353) },
                    { 0x70, new Instruction(LoadIYDR, TCycles.T44353) },
                    { 0x71, new Instruction(LoadIYDR, TCycles.T44353) },
                    { 0x72, new Instruction(LoadIYDR, TCycles.T44353) },
                    { 0x73, new Instruction(LoadIYDR, TCycles.T44353) },
                    { 0x74, new Instruction(LoadIYDR, TCycles.T44353) },
                    { 0x75, new Instruction(LoadIYDR, TCycles.T44353) },
                    { 0x77, new Instruction(LoadIYDR, TCycles.T44353) },
                    { 0x7E, new Instruction(LoadRIYD, TCycles.T44353) },
                    { 0x86, new Instruction(AddAIYD, TCycles.T44353) },
                    { 0x8E, new Instruction(AddWithCarryAIYD, TCycles.T44353) }
                })
            };

            _nonPrefixedInstructionGroup = new NonPrefixedInstructionGroup(new Dictionary<byte, Instruction>
            {
                { 0x00, new Instruction(NOP, TCycles.T4) },
                { 0x01, new Instruction(LoadDDNN, TCycles.T433) },
                { 0x02, new Instruction(LoadBCA, TCycles.T43) },
                { 0x04, new Instruction(IncrementR, TCycles.T4) },
                { 0x06, new Instruction(LoadRN, TCycles.T43) },
                { 0x07, new Instruction(RotateLeftCarryA, TCycles.T4) },
                { 0x0A, new Instruction(LoadABC, TCycles.T43) },
                { 0x0C, new Instruction(IncrementR, TCycles.T4) },
                { 0x0E, new Instruction(LoadRN, TCycles.T43) },
                { 0x11, new Instruction(LoadDDNN, TCycles.T433) },
                { 0x12, new Instruction(LoadDEA, TCycles.T43) },
                { 0x14, new Instruction(IncrementR, TCycles.T4) },
                { 0x16, new Instruction(LoadRN, TCycles.T43) },
                { 0x17, new Instruction(RotateLeftA, TCycles.T4) },
                { 0x1A, new Instruction(LoadADE, TCycles.T43) },
                { 0x1C, new Instruction(IncrementR, TCycles.T4) },
                { 0x1E, new Instruction(LoadRN, TCycles.T43) },
                { 0x21, new Instruction(LoadDDNN, TCycles.T433) },
                { 0x24, new Instruction(IncrementR, TCycles.T4) },
                { 0x26, new Instruction(LoadRN, TCycles.T43) },
                { 0x2C, new Instruction(IncrementR, TCycles.T4) },
                { 0x2E, new Instruction(LoadRN, TCycles.T43) },
                { 0x31, new Instruction(LoadDDNN, TCycles.T433) },
                { 0x32, new Instruction(LoadNNA, TCycles.T4333) },
                { 0x36, new Instruction(LoadHLN, TCycles.T433) },
                { 0x3A, new Instruction(LoadANN, TCycles.T4333) },
                { 0x3C, new Instruction(IncrementR, TCycles.T4) },
                { 0x3E, new Instruction(LoadRN, TCycles.T43) },

                { 0x40, new Instruction(LoadRR, TCycles.T4) },
                { 0x41, new Instruction(LoadRR, TCycles.T4) },
                { 0x42, new Instruction(LoadRR, TCycles.T4) },
                { 0x43, new Instruction(LoadRR, TCycles.T4) },
                { 0x44, new Instruction(LoadRR, TCycles.T4) },
                { 0x45, new Instruction(LoadRR, TCycles.T4) },
                { 0x46, new Instruction(LoadRHL, TCycles.T43) },
                { 0x47, new Instruction(LoadRR, TCycles.T4) },
                { 0x48, new Instruction(LoadRR, TCycles.T4) },
                { 0x49, new Instruction(LoadRR, TCycles.T4) },
                { 0x4A, new Instruction(LoadRR, TCycles.T4) },
                { 0x4B, new Instruction(LoadRR, TCycles.T4) },
                { 0x4C, new Instruction(LoadRR, TCycles.T4) },
                { 0x4D, new Instruction(LoadRR, TCycles.T4) },
                { 0x4E, new Instruction(LoadRHL, TCycles.T43) },
                { 0x4F, new Instruction(LoadRR, TCycles.T4) },
                { 0x50, new Instruction(LoadRR, TCycles.T4) },
                { 0x51, new Instruction(LoadRR, TCycles.T4) },
                { 0x52, new Instruction(LoadRR, TCycles.T4) },
                { 0x53, new Instruction(LoadRR, TCycles.T4) },
                { 0x54, new Instruction(LoadRR, TCycles.T4) },
                { 0x55, new Instruction(LoadRR, TCycles.T4) },
                { 0x56, new Instruction(LoadRHL, TCycles.T43) },
                { 0x57, new Instruction(LoadRR, TCycles.T4) },
                { 0x58, new Instruction(LoadRR, TCycles.T4) },
                { 0x59, new Instruction(LoadRR, TCycles.T4) },
                { 0x5A, new Instruction(LoadRR, TCycles.T4) },
                { 0x5B, new Instruction(LoadRR, TCycles.T4) },
                { 0x5C, new Instruction(LoadRR, TCycles.T4) },
                { 0x5D, new Instruction(LoadRR, TCycles.T4) },
                { 0x5E, new Instruction(LoadRHL, TCycles.T43) },
                { 0x5F, new Instruction(LoadRR, TCycles.T4) },
                { 0x60, new Instruction(LoadRR, TCycles.T4) },
                { 0x61, new Instruction(LoadRR, TCycles.T4) },
                { 0x62, new Instruction(LoadRR, TCycles.T4) },
                { 0x63, new Instruction(LoadRR, TCycles.T4) },
                { 0x64, new Instruction(LoadRR, TCycles.T4) },
                { 0x65, new Instruction(LoadRR, TCycles.T4) },
                { 0x66, new Instruction(LoadRHL, TCycles.T43) },
                { 0x67, new Instruction(LoadRR, TCycles.T4) },
                { 0x68, new Instruction(LoadRR, TCycles.T4) },
                { 0x69, new Instruction(LoadRR, TCycles.T4) },
                { 0x6A, new Instruction(LoadRR, TCycles.T4) },
                { 0x6B, new Instruction(LoadRR, TCycles.T4) },
                { 0x6C, new Instruction(LoadRR, TCycles.T4) },
                { 0x6D, new Instruction(LoadRR, TCycles.T4) },
                { 0x6E, new Instruction(LoadRHL, TCycles.T43) },
                { 0x6F, new Instruction(LoadRR, TCycles.T4) },
                { 0x70, new Instruction(LoadHLR, TCycles.T43) },
                { 0x71, new Instruction(LoadHLR, TCycles.T43) },
                { 0x72, new Instruction(LoadHLR, TCycles.T43) },
                { 0x73, new Instruction(LoadHLR, TCycles.T43) },
                { 0x74, new Instruction(LoadHLR, TCycles.T43) },
                { 0x75, new Instruction(LoadHLR, TCycles.T43) },
                { 0x77, new Instruction(LoadHLR, TCycles.T43) },
                { 0x78, new Instruction(LoadRR, TCycles.T4) },
                { 0x79, new Instruction(LoadRR, TCycles.T4) },
                { 0x7A, new Instruction(LoadRR, TCycles.T4) },
                { 0x7B, new Instruction(LoadRR, TCycles.T4) },
                { 0x7C, new Instruction(LoadRR, TCycles.T4) },
                { 0x7D, new Instruction(LoadRR, TCycles.T4) },
                { 0x7E, new Instruction(LoadRHL, TCycles.T43) },
                { 0x7F, new Instruction(LoadRR, TCycles.T4) },
                { 0x80, new Instruction(AddAR, TCycles.T4) },
                { 0x81, new Instruction(AddAR, TCycles.T4) },
                { 0x82, new Instruction(AddAR, TCycles.T4) },
                { 0x83, new Instruction(AddAR, TCycles.T4) },
                { 0x84, new Instruction(AddAR, TCycles.T4) },
                { 0x85, new Instruction(AddAR, TCycles.T4) },
                { 0x86, new Instruction(AddAHL, TCycles.T43) },
                { 0x87, new Instruction(AddAR, TCycles.T4) },
                { 0x88, new Instruction(AddWithCarryAR, TCycles.T4) },
                { 0x89, new Instruction(AddWithCarryAR, TCycles.T4) },
                { 0x8A, new Instruction(AddWithCarryAR, TCycles.T4) },
                { 0x8B, new Instruction(AddWithCarryAR, TCycles.T4) },
                { 0x8C, new Instruction(AddWithCarryAR, TCycles.T4) },
                { 0x8D, new Instruction(AddWithCarryAR, TCycles.T4) },
                { 0x8E, new Instruction(AddWithCarryAHL, TCycles.T43) },
                { 0x8F, new Instruction(AddWithCarryAR, TCycles.T4) },
                { 0x90, new Instruction(SubAR, TCycles.T4) },
                { 0x91, new Instruction(SubAR, TCycles.T4) },
                { 0x92, new Instruction(SubAR, TCycles.T4) },
                { 0x93, new Instruction(SubAR, TCycles.T4) },
                { 0x94, new Instruction(SubAR, TCycles.T4) },
                { 0x95, new Instruction(SubAR, TCycles.T4) },
                { 0x97, new Instruction(SubAR, TCycles.T4) },
                { 0xC2, new Instruction(JumpCCNN, TCycles.T433) },
                { 0xC3, new Instruction(JumpNN, TCycles.T433) },
                { 0xC6, new Instruction(AddAN, TCycles.T43) },
                { 0xCA, new Instruction(JumpCCNN, TCycles.T433) },
                { 0xCE, new Instruction(AddWithCarryAN, TCycles.T43) },
                { 0xD2, new Instruction(JumpCCNN, TCycles.T433) },
                { 0xDA, new Instruction(JumpCCNN, TCycles.T433) },
                { 0xE2, new Instruction(JumpCCNN, TCycles.T433) },
                { 0xEA, new Instruction(JumpCCNN, TCycles.T433) },
                { 0xF2, new Instruction(JumpCCNN, TCycles.T433) },
                { 0xFA, new Instruction(JumpCCNN, TCycles.T433) }
            });
        }

        public uint Step()
        {
            var opCode = _memory.ReadByte(Registers.ProgramCounter++);

            var prefixedInstructionGroup = _prefixedInstructionGroups.FirstOrDefault(x => x.Prefix == opCode);
            return prefixedInstructionGroup?.Execute(_memory.ReadByte(Registers.ProgramCounter++))
                   ?? _nonPrefixedInstructionGroup.Execute(opCode);
        }

        private void NOP(byte opCode)
        { }
    }
}