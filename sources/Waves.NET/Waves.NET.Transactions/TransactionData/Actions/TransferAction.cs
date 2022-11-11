namespace Waves.NET.Transactions.Actions
{
    public record TransferAction
    {
        public string Address { get; init; } = null!;
        public string Asset { get; init; } = null!;
        public long Amount { get; init; }
    }
}