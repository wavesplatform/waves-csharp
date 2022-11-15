using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public record Payment
    {
        public Base58s AssetId { get; init; } = null!;
        public long Amount { get; init; }
    }
}