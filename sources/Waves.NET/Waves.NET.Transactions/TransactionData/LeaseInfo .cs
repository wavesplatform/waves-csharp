using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public record LeaseInfo
    {
        public string Id { get; init; } = null!;
        public string OriginTransactionId { get; init; } = null!;
        public string Sender { get; init; } = null!;
        public string Recipient { get; init; } = null!;
        public long Amount { get; init; }
        public int Height { get; init; }
        public LeaseStatus Status { get; init; }
        public int CancelHeight { get; init; }
        public Base58s? CancelTransactionId { get; init; } = null!;
    }
}
