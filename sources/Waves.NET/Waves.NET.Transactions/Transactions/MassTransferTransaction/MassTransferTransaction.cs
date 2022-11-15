using Waves.NET.Transactions.Common;
using Waves.NET.Transactions;

namespace Waves.NET.Transactions
{
    public class MassTransferTransaction : Transaction, IMassTransferTransaction
    {
        public const int TYPE = 11;
        public const int LatestVersion = 2;
        public const int MinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public Base58s Attachment { get; set; } = null!;
        public int TransferCount { get; set; }
        public long TotalAmount { get; set; }
        public ICollection<Transfer> Transfers { get; set; } = null!;
    }
}