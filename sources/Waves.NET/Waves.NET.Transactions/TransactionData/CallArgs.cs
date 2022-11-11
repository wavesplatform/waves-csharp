namespace Waves.NET.Transactions
{
    public record CallArgs
    {
        public string Type { get; init; } = null!;
        public string Value { get; init; } = null!;
    }
}