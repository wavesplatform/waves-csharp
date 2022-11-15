using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface ISponsorFeeTransaction : INonGenesisTransaction
    {
        Base58s? AssetId { get; set; }
        long MinSponsoredAssetFee { get; set; }
    }
}