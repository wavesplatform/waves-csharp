using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WavesLabs.Node.Transactions.JsonConverters
{
    public class EthTransactionPayloadJsonConverter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            EthTransactionPayload? payload = null;

            if (reader.TokenType == JsonToken.StartObject)
            {
                var jO = JObject.Load(reader);
                var type = Enum.Parse<EthTransactionPayloadType>(jO.Value<string>("type"), true);

                switch (type)
                {
                    case EthTransactionPayloadType.Invocation: payload = new EthTransactionInvokePayload(); break;
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