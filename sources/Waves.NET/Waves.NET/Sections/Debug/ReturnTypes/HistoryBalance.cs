namespace Waves.NET.Debug
{
    public record HistoryBalance
    {
        public int Height { get; init; }
        public long Balance { get; init; }
    }
}
