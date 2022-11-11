namespace Waves.NET.Assets.ReturnTypes
{
    public record AssetDetails
    {
        public string AssetId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Decimals { get; set; }
        public int IssueHeight { get; set; }
        public long IssueTimestamp { get; set; }
        public string Issuer { get; set; } = null!;
        public string IssuerPublicKey { get; set; } = null!;
        public bool Reissuable { get; set; }
        public bool Scripted { get; set; }
        public long MinSponsoredAssetFee { get; set; }
        public string OriginTransactionId { get; set; } = null!;
        public ScriptDetails? ScriptDetails { get; set; }
    }
}
