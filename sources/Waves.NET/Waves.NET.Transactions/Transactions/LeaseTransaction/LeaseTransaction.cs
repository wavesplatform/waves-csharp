using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class LeaseTransaction : Transaction, ILeaseTransaction
    {
        public const int TYPE = 8;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public IRecipient Recipient { get; set; } = null!;
        public long Amount { get; set; }

        public LeaseStatus Status { get; set; }
    }
}