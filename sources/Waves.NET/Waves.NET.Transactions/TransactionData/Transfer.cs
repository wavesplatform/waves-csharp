namespace Waves.NET.Transactions
{
    public record Transfer
    {
        public string Recipient { get; init; } = null!;
        public long Amount { get; init; }
    }
}