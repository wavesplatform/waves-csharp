using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public record Amount
    {
        public Base58s? AssetId { get; init; }
        public long Value { get; init; }

        public Amount(long value, Base58s? assetId)
        {
            AssetId = assetId;
            Value = value;
        }

        public static Amount Of(long value, Base58s? assetId)
        {
            return new Amount(value, assetId);
        }

        public static implicit operator long(Amount a) => a.Value;
        public static explicit operator Amount(long v) => new(v, Base58s.Empty);
    }
}