using Newtonsoft.Json;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions.JsonConverters
{
    public class RecipientJsonConverter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String || string.IsNullOrWhiteSpace((string?)reader.Value)) return null;

            var str = (string)reader.Value;

            return Address.IsAddress(str)
                ? Address.As(str)
                : Alias.As(str);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }

        public override bool CanConvert(Type objectType) => false;
    }
}
