using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public record AssetPair
    {
        public Base58s? AmountAsset { get; init; }
        public Base58s? PriceAsset { get; init; }
    }
}