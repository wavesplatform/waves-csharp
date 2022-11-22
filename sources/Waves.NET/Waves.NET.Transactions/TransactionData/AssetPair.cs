using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public record AssetPair
    {
        public Base58s? AmountAsset { get; init; }
        public Base58s? PriceAsset { get; init; }
    }
}