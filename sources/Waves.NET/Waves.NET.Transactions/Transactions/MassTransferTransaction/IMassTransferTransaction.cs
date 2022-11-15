using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface IMassTransferTransaction : INonGenesisTransaction
    {
        Base58s? AssetId { get; set; }
        Base58s Attachment { get; set; }
        int TransferCount { get; set; }
        long TotalAmount { get; set; }
        ICollection<Transfer> Transfers { get; set; }
    }
}