namespace WavesLabs.Node.Transactions
{
    public record BooleanEntry : DataEntry
    {
        public bool Value { get; init; }

        public BooleanEntry()
        {
            Type = "boolean";
        }
    }
}
