using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Waves.NET.Transactions.JsonConverters
{
    public class EthTransactionPayloadJsonConverter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            EthTransactionPayload? payload = null;

            if (reader.TokenType == JsonToken.StartObject)
            {
                var jO = JObject.Load(reader);
                var type = jO.Value<int>("type");

                switch ((EthTransactionPayloadType)type)
                {
                    case EthTransactionPayloadType.Invoke:payload = new EthTransactionInvokePayload(); break;
                    case EthTransactionPayloadType.Transfer: payload = new EthTransactionTransferPayload(); break;
                }

                if (payload is not null)
                {
                    serializer.Populate(jO.CreateReader(), payload);
                }
            }

            return payload;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableTo(typeof(EthTransactionPayload));
        }
    }
}