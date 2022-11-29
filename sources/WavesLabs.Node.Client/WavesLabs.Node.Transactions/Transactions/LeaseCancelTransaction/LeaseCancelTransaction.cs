using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class LeaseCancelTransaction : Transaction, ILeaseCancelTransaction, IEquatable<LeaseCancelTransaction?>
    {
        public const int TYPE = 9;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public Base58s LeaseId { get; set; } = null!;
        public LeaseInfo? Lease { get; set; }

        public LeaseCancelTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as LeaseCancelTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as LeaseCancelTransaction);
        }

        public bool Equals(LeaseCancelTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<Base58s>.Default.Equals(LeaseId, other.LeaseId) &&
                   EqualityComparer<LeaseInfo?>.Default.Equals(Lease, other.Lease);
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), LeaseId, Lease);
        public static bool operator ==(LeaseCancelTransaction? left, LeaseCancelTransaction? right) => EqualityComparer<LeaseCancelTransaction>.Default.Equals(left, right);
        public static bool operator !=(LeaseCancelTransaction? left, LeaseCancelTransaction? right) => !(left == right);
    }
}