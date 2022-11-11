namespace Waves.NET.Transactions
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
