namespace Waves.NET.Transactions
{
    public class ReissueTransaction : Transaction, IReissueTransaction
    {
        public const int TYPE = 5;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public string AssetId { get; set; } = null!;
        public long Quantity { get; set; }
        public bool Reissuable { get; set; }
    }

    public interface IReissueTransaction : INonGenesisTransaction
    {
        string AssetId { get; set; }
        long Quantity { get; set; }
        bool Reissuable { get; set; }
    }
}