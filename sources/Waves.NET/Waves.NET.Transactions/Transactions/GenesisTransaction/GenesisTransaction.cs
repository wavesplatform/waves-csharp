using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class GenesisTransaction : Transaction, IGenesisTransaction, IEquatable<GenesisTransaction?>
    {
        public const int TYPE = 1;
        public const int LatestVersion = 1;

        public Address Recipient { get; set; } = null!;
        public long Amount { get; set; }

        public GenesisTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as GenesisTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as GenesisTransaction);
        }

        public bool Equals(GenesisTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<Address>.Default.Equals(Recipient, other.Recipient) &&
                   Amount == other.Amount;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Recipient, Amount);
        public static bool operator ==(GenesisTransaction? left, GenesisTransaction? right) => EqualityComparer<GenesisTransaction>.Default.Equals(left, right);
        public static bool operator !=(GenesisTransaction? left, GenesisTransaction? right) => !(left == right);
    }
}