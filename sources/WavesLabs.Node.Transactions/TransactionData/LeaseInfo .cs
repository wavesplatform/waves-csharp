using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public record LeaseInfo
    {
        public Base58s Id { get; init; } = null!;
        public Base58s OriginTransactionId { get; init; } = null!;
        public string Sender { get; init; } = null!;
        public string Recipient { get; init; } = null!;
        public long Amount { get; init; }
        public int Height { get; init; }
        public LeaseStatus Status { get; init; }
        public int? CancelHeight { get; init; }
        public Base58s? CancelTransactionId { get; init; } = null!;
    }
}
