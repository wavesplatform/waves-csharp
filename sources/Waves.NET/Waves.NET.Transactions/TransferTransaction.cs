namespace Waves.NET.Transactions
{
    public class TransferTransaction : Transaction, ITransferTransaction
    {
        public const int TYPE = 4;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public string Recipient { get; set; } = null!;
        public string AssetId { get; set; } = null!;
        public string FeeAsset { get; set; } = null!;
        public long Amount { get; set; }
        public string Attachment { get; set; } = null!;
    }

    public interface ITransferTransaction : INonGenesisTransaction
    {
        string Recipient { get; set; }
        string AssetId { get; set; }
        string FeeAsset { get; set; }
        long Amount { get; set; }
        string Attachment { get; set; }
    }
}