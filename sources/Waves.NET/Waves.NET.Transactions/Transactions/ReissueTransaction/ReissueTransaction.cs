using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class ReissueTransaction : Transaction, IReissueTransaction
    {
        public const int TYPE = 5;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public long Amount { get; set; }
        public long Quantity { get; set; }
        public bool Reissuable { get; set; }
    }
}