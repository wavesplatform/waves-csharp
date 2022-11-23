using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class ReissueTransaction : Transaction, IReissueTransaction, IEquatable<ReissueTransaction?>
    {
        public const int TYPE = 5;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public long Amount { get; set; }
        public long Quantity { get; set; }
        public bool Reissuable { get; set; }

        public ReissueTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as ReissueTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as ReissueTransaction);
        }

        public bool Equals(ReissueTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<Base58s?>.Default.Equals(AssetId, other.AssetId) &&
                   Amount == other.Amount &&
                   Quantity == other.Quantity &&
                   Reissuable == other.Reissuable;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), AssetId, Amount, Quantity, Reissuable);
        public static bool operator ==(ReissueTransaction? left, ReissueTransaction? right) => EqualityComparer<ReissueTransaction>.Default.Equals(left, right);
        public static bool operator !=(ReissueTransaction? left, ReissueTransaction? right) => !(left == right);
    }
}