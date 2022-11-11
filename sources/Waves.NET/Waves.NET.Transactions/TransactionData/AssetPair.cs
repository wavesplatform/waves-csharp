namespace Waves.NET.Transactions
{
    public record AssetPair
    {
        public string? AmountAsset { get; init; }
        public string? PriceAsset { get; init; }
    }
}