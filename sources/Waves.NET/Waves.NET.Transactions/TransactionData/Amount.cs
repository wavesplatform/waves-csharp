namespace Waves.NET.Transactions
{
    public record Amount
    {
        public string AssetId { get; init; } = null!;
        public long Value { get; init; }

        public Amount(long value, string assetId)
        {
            AssetId = assetId;
            Value = value;
        }

        public static Amount Of(long value, string assetId)
        {
            return new Amount(value, assetId);
        }

        public static implicit operator long(Amount a) => a.Value;
        public static explicit operator Amount(long v) => new(v, "");
    }
}