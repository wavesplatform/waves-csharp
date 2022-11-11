namespace Waves.NET.Transactions
{
    public record Order
    {
        public int Version { get; init; }
        public string Id { get; init; } = "";
        public string Sender { get; init; } = "";
        public string SenderPublicKey { get; init; } = "";
        public string MatcherPublicKey { get; init; } = "";
        public AssetPair AssetPair { get; init; } = new AssetPair();
        public OrderType OrderType { get; init; }
        public long Amount { get; init; }
        public long Price { get; init; }
        public long Timestamp { get; init; }
        public long Expiration { get; init; }
        public long MatcherFee { get; init; }
        public string Signature { get; init; } = "";
        public ICollection<string> Proofs { get; init; } = new List<string>();
        public string MatcherFeeAssetId { get; init; } = "";
    }
}