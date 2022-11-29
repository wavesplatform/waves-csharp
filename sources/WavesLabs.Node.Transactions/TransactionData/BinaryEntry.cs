using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public record BinaryEntry : DataEntry
    {
        public Base64s Value { get; init; } = null!;

        public BinaryEntry()
        {
            Type = "binary";
        }
    }
}
