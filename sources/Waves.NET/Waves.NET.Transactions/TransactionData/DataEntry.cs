namespace Waves.NET.Transactions
{
    public abstract record DataEntry : EntryData
    {
        public string Type { get; init; } = null!;
    }
}
