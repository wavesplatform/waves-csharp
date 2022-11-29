using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class LeaseTransaction : Transaction, ILeaseTransaction, IEquatable<LeaseTransaction?>
    {
        public const int TYPE = 8;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public IRecipient Recipient { get; set; } = null!;
        public long Amount { get; set; }

        public LeaseStatus Status { get; set; }

        public LeaseTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as LeaseTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as LeaseTransaction);
        }

        public bool Equals(LeaseTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<IRecipient>.Default.Equals(Recipient, other.Recipient) &&
                   Amount == other.Amount &&
                   Status == other.Status;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Recipient, Amount, Status);
        public static bool operator ==(LeaseTransaction? left, LeaseTransaction? right) => EqualityComparer<LeaseTransaction>.Default.Equals(left, right);
        public static bool operator !=(LeaseTransaction? left, LeaseTransaction? right) => !(left == right);
    }
}