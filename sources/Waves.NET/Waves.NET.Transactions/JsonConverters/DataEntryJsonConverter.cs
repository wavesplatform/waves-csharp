using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions.JsonConverters
{
    public class DataEntryJsonConverter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                var jt = JToken.Load(reader);
                var key = jt.Value<string>("key");
                var type = jt.Value<string>("type");

                if(string.IsNullOrWhiteSpace(type))
                {
                    return new DeleteEntry { Key = key!, Value = jt.Value<string>("value") };
                }

                switch (type.ToLower())
                {
                    case "string": return new StringEntry { Key = key!, Type = type, Value = jt.Value<string>("value")! };
                    case "boolean": return new BooleanEntry { Key = key!, Type = type, Value = jt.Value<bool>("value")! };
                    case "integer": return new IntegerEntry { Key = key!, Type = type, Value = jt.Value<long>("value")! };
                    case "binary": return new BinaryEntry { Key = key!, Type = type, Value = Base64.As(jt.Value<string>("value")!) };
                }
            }

            return null!;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
