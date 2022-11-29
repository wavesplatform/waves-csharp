using Newtonsoft.Json;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public record Amount
    {
        public Base58s? AssetId { get; init; } // null -> WAVES
        [JsonProperty("amount")]
        public long Value { get; init; }

        public static Amount As(long value, Base58s? assetId) => new Amount { AssetId = assetId, Value = value };
    }
}