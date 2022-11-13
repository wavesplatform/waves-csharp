namespace Waves.NET.Transactions
{
    public class IssueTransaction : Transaction, IIssueTransaction
    {
        public const int TYPE = 3;
        public const int LatestVersion = 3;
        public const int MinFee = 100000000;
        public const int NftMinFee = 100000;

        public string AssetId { get; set; } = "";
        public long Amount { get; set; }
        public string Name { get; set; } = "";
        public long Quantity { get; set; }
        public bool Reissuable { get; set; }
        public int Decimals { get; set; }
        public string Description { get; set; } = "";
        public string Script { get; set; } = "";
    }

    public interface IIssueTransaction : INonGenesisTransaction
    {
        string AssetId { get; set; }
        string Name { get; set; }
        long Amount { get; set; }
        long Quantity { get; set; }
        bool Reissuable { get; set; }
        int Decimals { get; set; }
        string Description { get; set; }
        string Script { get; set; }
    }
}