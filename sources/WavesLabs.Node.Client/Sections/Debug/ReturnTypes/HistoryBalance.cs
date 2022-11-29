namespace WavesLabs.Node.Client.ReturnTypes
{
    public record HistoryBalance
    {
        public int Height { get; init; }
        public long Balance { get; init; }
    }
}
