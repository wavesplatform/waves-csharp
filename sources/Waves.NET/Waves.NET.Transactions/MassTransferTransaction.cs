namespace Waves.NET.Transactions
{
    public class MassTransferTransaction : Transaction, IMassTransferTransaction
    {
        public const int TYPE = 11;
        public const int LatestVersion = 2;
        public const int MinFee = 100000;

        public string AssetId { get; set; } = null!;
        public string Attachment { get; set; } = null!;
        public int TransferCount { get; set; }
        public long TotalAmount { get; set; }
        public ICollection<Transfer> Transfers { get; set; } = null!;
    }

    public interface IMassTransferTransaction : INonGenesisTransaction
    {
        string AssetId { get; set; }
        string Attachment { get; set; }
        int TransferCount { get; set; }
        long TotalAmount { get; set; }
        ICollection<Transfer> Transfers { get; set; }
    }
}