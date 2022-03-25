using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Zega.Tests.Fuse.JsonFormat
{
    public partial class FuseTestCase
    {
        [JsonProperty("TestDescription")]
        public string TestDescription { get; set; } 

        [JsonProperty("AF")]
        public ushort Af { get; set; }

        [JsonProperty("BC")]
        public ushort Bc { get; set; }

        [JsonProperty("DE")]
        public ushort De { get; set; }

        [JsonProperty("HL")]
        public ushort Hl { get; set; }

        [JsonProperty("ShadowAF")]
        public ushort ShadowAf { get; set; }

        [JsonProperty("ShadowBC")]
        public ushort ShadowBc { get; set; }

        [JsonProperty("ShadowDE")]
        public ushort ShadowDe { get; set; }

        [JsonProperty("ShadowHL")]
        public ushort ShadowHl { get; set; }

        [JsonProperty("IndexX")]
        public ushort IndexX { get; set; }

        [JsonProperty("IndexY")]
        public ushort IndexY { get; set; }

        [JsonProperty("StackPointer")]
        public ushort StackPointer { get; set; }

        [JsonProperty("ProgramCounter")]
        public ushort ProgramCounter { get; set; }

        [JsonProperty("InterruptVector")]
        public byte InterruptVector { get; set; }

        [JsonProperty("MemoryRefresh")]
        public byte MemoryRefresh { get; set; }

        [JsonProperty("InterruptFlipFlop1")]
        public bool InterruptFlipFlop1 { get; set; }

        [JsonProperty("InterruptFlipFlop2")]
        public bool InterruptFlipFlop2 { get; set; }

        [JsonProperty("InterruptMode")]
        public byte InterruptMode { get; set; }

        [JsonProperty("Halted")]
        public bool Halted { get; set; }

        [JsonProperty("Cycles")]
        public uint Cycles { get; set; }

        [JsonProperty("MemoryBlocks")]
        public List<MemoryBlock> MemoryBlocks { get; set; }
    }

    public partial class MemoryBlock
    {
        [JsonProperty("StartAddress")]
        public ushort StartAddress { get; set; }

        [JsonProperty("Bytes")]
        public List<byte> Bytes { get; set; }
    }

    public partial class FuseTestCase
    {
        public static List<FuseTestCase> FromJson(string json) => JsonConvert.DeserializeObject<List<FuseTestCase>>(json, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new()
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
