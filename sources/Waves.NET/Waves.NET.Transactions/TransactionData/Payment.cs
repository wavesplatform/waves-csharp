namespace Waves.NET.Transactions
{
    public record Payment
    {
        public string AssetId { get; init; } = null!;
        public long Amount { get; init; }
    }
}