using Newtonsoft.Json;

namespace WavesLabs.Node.Transactions.JsonConverters
{
    public class StringJsonConverter : JsonConverter
    {
        public override bool CanRead => false;
        public override bool CanConvert(Type objectType) => false;

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }
    }
}
