using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class TransferTransaction : Transaction, ITransferTransaction
    {
        public const int TYPE = 4;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public IRecipient Recipient { get; set; } = null!;
        public Base58s? AssetId { get; set; }
        public Base58s? FeeAsset { get; set; }
        public long Amount { get; set; }
        public Base58s? Attachment { get; set; } = null!;
    }
}