namespace Waves.NET.Assets.ReturnTypes
{
    public record AssetBalanceAndDetails
    {
        public string AssetId { get; set; } = null!;
        public long Balance { get; set; }
        public bool Reissuable { get; set; }
        public long MinSponsoredAssetFee { get; set; }
        public long SponsorBalance { get; set; }
        public long Quantity { get; set; }
        public IssueTransaction IssueTransaction { get; set; } = null!;
    }
}
