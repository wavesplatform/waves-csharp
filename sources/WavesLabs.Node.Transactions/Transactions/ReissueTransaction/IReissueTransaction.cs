using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface IReissueTransaction : INonGenesisTransaction
    {
        AssetId? AssetId { get; set; }
        long Amount { get; set; }
        long Quantity { get; set; }
        bool Reissuable { get; set; }
    }
}