namespace WavesLabs.Node.Transactions
{
    public record IntegerEntry : DataEntry
    {
        public long Value { get; init; }

        public IntegerEntry()
        {
            Type = "integer";
        }
    }
}
