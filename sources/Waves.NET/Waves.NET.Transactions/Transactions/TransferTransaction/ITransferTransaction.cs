using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface ITransferTransaction : INonGenesisTransaction
    {
        IRecipient Recipient { get; set; }
        Base58s? AssetId { get; set; }
        Base58s? FeeAsset { get; set; }
        long Amount { get; set; }
        string Attachment { get; set; }
    }
}