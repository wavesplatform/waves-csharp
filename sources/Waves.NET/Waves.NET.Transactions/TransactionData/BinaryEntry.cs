namespace Waves.NET.Transactions
{
    public record BinaryEntry : DataEntry
    {
        public string Value { get; init; } = null!;

        public BinaryEntry()
        {
            Type = "binary";
        }
    }
}
