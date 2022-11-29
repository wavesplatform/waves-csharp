using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface ISponsorFeeTransaction : INonGenesisTransaction
    {
        Base58s? AssetId { get; set; }
        long MinSponsoredAssetFee { get; set; }
    }
}