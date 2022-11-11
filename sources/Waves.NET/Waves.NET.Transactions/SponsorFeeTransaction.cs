namespace Waves.NET.Transactions
{
    public class SponsorFeeTransaction : Transaction, ISponsorFeeTransaction
    {
        public const int TYPE = 14;
        public const int LatestVersion = 2;
        public const int MinFee = 100000;

        public string AssetId { get; set; } = null!;
        public long MinSponsoredAssetFee { get; set; }
    }

    public interface ISponsorFeeTransaction : INonGenesisTransaction
    {
        string AssetId { get; set; }
        long MinSponsoredAssetFee { get; set; }
    }
}