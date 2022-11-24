using Newtonsoft.Json;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public record Amount
    {
        public Base58s? AssetId { get; init; } // null -> WAVES
        [JsonProperty("amount")]
        public long Value { get; init; }

        public static Amount As(long value, Base58s? assetId) => new Amount { AssetId = assetId, Value = value };
    }
}