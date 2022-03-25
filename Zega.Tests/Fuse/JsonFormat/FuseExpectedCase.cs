using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Zega.Tests.Fuse.OriginalFormat;

namespace Zega.Tests.Fuse.JsonFormat
{
    public partial class FuseExpectedCase
    {
        [JsonProperty("TestDescription")]
        public string TestDescription { get; set; }

        [JsonProperty("Events")]
        public List<Event> Events { get; set; }

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

        [JsonProperty("ExpectedMemoryBlocks")]
        public List<ExpectedMemoryBlock> ExpectedMemoryBlocks { get; set; }
    }

    public class Event
    {
        [JsonProperty("Time")]
        public uint Time { get; set; }

        [JsonProperty("EventType", ItemConverterType = typeof(NumberToEventTypeConverter))]
        public FuseEventType EventType { get; set; }

        [JsonProperty("Address")]
        public ushort Address { get; set; }

        [JsonProperty("Data")]
        public byte? Data { get; set; }
    }

    public class ExpectedMemoryBlock
    {
        [JsonProperty("StartAddress")]
        public ushort StartAddress { get; set; }

        [JsonProperty("Bytes")]
        public List<byte> Bytes { get; set; }
    }

    public class NumberToEventTypeConverter : JsonConverter<FuseEventType>
    {
        public override void WriteJson(JsonWriter writer, FuseEventType value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override FuseEventType ReadJson(JsonReader reader, Type objectType, FuseEventType existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            return (FuseEventType) reader.ReadAsInt32().Value;
        }
    }

    public partial class FuseExpectedCase
    {
        public static List<FuseExpectedCase> FromJson(string json) => JsonConvert.DeserializeObject<List<FuseExpectedCase>>(json, new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        });
    }
}
