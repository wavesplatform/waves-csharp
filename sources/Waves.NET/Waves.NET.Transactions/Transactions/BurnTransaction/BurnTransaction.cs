using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class BurnTransaction : Transaction, IBurnTransaction, IEquatable<BurnTransaction?>
    {
        public const int TYPE = 6;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public long Amount { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as BurnTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as BurnTransaction);
        }

        public bool Equals(BurnTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<Base58s?>.Default.Equals(AssetId, other.AssetId) &&
                   Amount == other.Amount;
        }

        public static bool operator ==(BurnTransaction? left, BurnTransaction? right) => EqualityComparer<BurnTransaction>.Default.Equals(left, right);
        public static bool operator !=(BurnTransaction? left, BurnTransaction? right) => !(left == right);
        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), AssetId, Amount);
    }
}