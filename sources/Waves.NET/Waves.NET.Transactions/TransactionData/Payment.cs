using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public record Payment
    {
        public Base58s? AssetId { get; init; }
        public long Amount { get; init; }

        public static Payment As(long amount, Base58s? assetId) => new Payment { Amount = amount, AssetId = assetId };
    }
}