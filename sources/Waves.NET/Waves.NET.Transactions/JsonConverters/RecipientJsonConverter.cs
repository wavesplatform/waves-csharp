using Newtonsoft.Json;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions.JsonConverters
{
    public class RecipientJsonConverter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String || string.IsNullOrWhiteSpace((string?)reader.Value)) return null;

            var str = (string)reader.Value;

            return Alias.IsAlias(str)
                ? Alias.As(str)
                : Address.As(str);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            writer.WriteValue(value?.ToString());
        }

        public override bool CanConvert(Type objectType) => false;
    }
}
