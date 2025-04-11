using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface ITransferTransaction : INonGenesisTransaction
    {
        IRecipient Recipient { get; set; }
        AssetId? AssetId { get; set; }
        AssetId? FeeAsset { get; set; }
        long Amount { get; set; }
        string Attachment { get; set; }
    }
}