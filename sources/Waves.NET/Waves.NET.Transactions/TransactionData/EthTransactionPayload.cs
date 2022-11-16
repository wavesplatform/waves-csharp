using Newtonsoft.Json;
using Waves.NET.Transactions.JsonConverters;

namespace Waves.NET.Transactions
{
    [JsonConverter(typeof(EthTransactionPayloadJsonConverter))]
    public abstract record EthTransactionPayload
    {
        public EthTransactionPayloadType Type { get; init; }
    }
}