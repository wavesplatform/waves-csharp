namespace WavesLabs.Node.Transactions.Common
{
    public record NameTypePair
    {
        public string Name { get; init; } = null!;
        public string Type { get; init; } = null!;
    }
}
