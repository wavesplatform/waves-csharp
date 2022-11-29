namespace WavesLabs.Node.Transactions
{
    public record DeleteEntry : EntryData
    {
        public object? Value { get; init; }
    }
}
