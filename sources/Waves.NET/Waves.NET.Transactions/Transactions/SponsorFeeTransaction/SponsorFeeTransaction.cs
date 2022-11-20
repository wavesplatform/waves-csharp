using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class SponsorFeeTransaction : Transaction, ISponsorFeeTransaction
    {
        public const int TYPE = 14;
        public const int LatestVersion = 2;
        public const int MinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public long MinSponsoredAssetFee { get; set; }
    }
}