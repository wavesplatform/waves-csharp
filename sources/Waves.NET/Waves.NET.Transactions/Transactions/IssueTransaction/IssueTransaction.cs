using Waves.NET.Transactions.Common;
using Waves.NET.Transactions;

namespace Waves.NET.Transactions
{
    public class IssueTransaction : Transaction, IIssueTransaction
    {
        public const int TYPE = 3;
        public const int LatestVersion = 3;
        public const int MinFee = 100000000;
        public const int NftMinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public long Amount { get; set; }
        public string Name { get; set; } = "";
        public long Quantity { get; set; }
        public bool Reissuable { get; set; }
        public int Decimals { get; set; }
        public string Description { get; set; } = "";
        public string Script { get; set; } = "";
    }
}