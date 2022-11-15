using Waves.NET.Transactions.Common;
using Waves.NET.Transactions;

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