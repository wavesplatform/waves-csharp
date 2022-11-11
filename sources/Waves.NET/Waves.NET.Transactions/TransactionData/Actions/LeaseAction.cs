namespace Waves.NET.Transactions.Actions
{
    public record LeaseAction
    {
        public string Id { get; init; } = "";
        public string OriginTransactionId { get; init; } = "";
        public string Sender { get; init; } = "";
        public string Recipient { get; init; } = "";
        public int Amount { get; init; }
        public int Height { get; init; }
        public LeaseStatus Status { get; init; }
        public int CancelHeight { get; init; }
        public string CancelTransactionId { get; init; } = "";
    }
}