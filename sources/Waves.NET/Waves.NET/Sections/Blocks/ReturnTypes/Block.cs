using Waves.NET.Transactions.Info;

namespace Waves.NET.ReturnTypes
{
    public class Block : BlockHeader, IEquatable<Block?>
    {
        public ICollection<TransactionInfo> Transactions { get; init; } = null!;
        public long Fee;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as Block is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Block);
        }

        public bool Equals(Block? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   (Transactions is null && other.Transactions is null || Transactions.SequenceEqual(other.Transactions));
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Transactions);
        public static bool operator ==(Block? left, Block? right) => EqualityComparer<Block>.Default.Equals(left, right);
        public static bool operator !=(Block? left, Block? right) => !(left == right);
    }
}
