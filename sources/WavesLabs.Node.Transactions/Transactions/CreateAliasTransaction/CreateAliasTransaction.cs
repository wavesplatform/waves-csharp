using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class CreateAliasTransaction : Transaction, ICreateAliasTransaction, IEquatable<CreateAliasTransaction?>
    {
        public const int TYPE = 10;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public Alias Alias { get; set; } = null!;

        public CreateAliasTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as CreateAliasTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as CreateAliasTransaction);
        }

        public bool Equals(CreateAliasTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   Alias == other.Alias;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Alias);
        public static bool operator ==(CreateAliasTransaction? left, CreateAliasTransaction? right)
            => EqualityComparer<CreateAliasTransaction>.Default.Equals(left, right);
        public static bool operator !=(CreateAliasTransaction? left, CreateAliasTransaction? right) => !(left == right);
    }
}