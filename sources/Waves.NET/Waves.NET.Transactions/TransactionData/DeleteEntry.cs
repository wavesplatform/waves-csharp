namespace Waves.NET.Transactions
{
    public record DeleteEntry : EntryData
    {
        public string? Value { get; init; }
    }
}
