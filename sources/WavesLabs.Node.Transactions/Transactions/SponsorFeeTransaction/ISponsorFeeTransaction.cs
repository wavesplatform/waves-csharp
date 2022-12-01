using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface ISponsorFeeTransaction : INonGenesisTransaction
    {
        AssetId? AssetId { get; set; }
        long MinSponsoredAssetFee { get; set; }
    }
}