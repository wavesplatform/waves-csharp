using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions.Actions
{
    public record LeaseAction
    {
        public Base58s Id { get; init; } = null!;
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