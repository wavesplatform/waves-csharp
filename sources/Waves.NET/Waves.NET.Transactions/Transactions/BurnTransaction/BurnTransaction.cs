using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class BurnTransaction : Transaction, IBurnTransaction
    {
        public const int TYPE = 6;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public long Amount { get; set; }
    }
}