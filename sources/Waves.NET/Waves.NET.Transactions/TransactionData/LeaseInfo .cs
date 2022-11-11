namespace Waves.NET.Transactions
{
    public record LeaseInfo
    {
        public string Id { get; init; } = null!;
        public string OriginTransactionId { get; init; } = null!;
        public string Sender { get; init; } = null!;
        public string Recipient { get; init; } = null!;
        public int Amount { get; init; }
        public int Height { get; init; }
        public LeaseStatus Status { get; init; }
        public int CancelHeight { get; init; }
        public string CancelTransactionId { get; init; } = null!;
    }
}
