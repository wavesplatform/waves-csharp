using Newtonsoft.Json;
using WavesLabs.Node.Transactions.JsonConverters;

namespace WavesLabs.Node.Transactions
{
    [JsonConverter(typeof(EthTransactionPayloadJsonConverter))]
    public abstract record EthTransactionPayload
    {
        public EthTransactionPayloadType Type { get; init; }
    }
}