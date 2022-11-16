namespace Waves.NET.Transactions
{
    public record DeleteEntry : EntryData
    {
        public object? Value { get; init; }
    }
}
