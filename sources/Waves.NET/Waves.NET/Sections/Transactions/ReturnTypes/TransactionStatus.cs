namespace Waves.NET.Transactions
{
    public record TransactionStatus
    {
        public string Id { get; init; } = null!;
        public string Status { get; init; } = null!;
        public int Height { get; init; }
        public int Confirmations { get; init; }
        public ApplicationStatus ApplicationStatus { get; init; }
    }
}