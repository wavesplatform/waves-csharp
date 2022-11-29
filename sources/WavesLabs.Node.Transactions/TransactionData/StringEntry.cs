namespace WavesLabs.Node.Transactions
{
    public record StringEntry : DataEntry
    {
        public string Value { get; init; } = null!;

        public StringEntry()
        {
            Type = "string";
        }
    }
}
