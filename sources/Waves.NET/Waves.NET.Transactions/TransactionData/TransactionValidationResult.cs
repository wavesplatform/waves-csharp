namespace Waves.NET.Transactions
{
    public record TransactionValidationResult
    {
        public bool Valid { get; init; }
        public int ValidationTime { get; init; }
        public ICollection<string> Trace { get; init; } = null!;
    }
}
