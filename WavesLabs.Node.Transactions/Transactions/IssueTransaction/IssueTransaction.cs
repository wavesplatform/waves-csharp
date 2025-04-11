using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class IssueTransaction : Transaction, IIssueTransaction, IEquatable<IssueTransaction?>
    {
        public const int TYPE = 3;
        public const int LatestVersion = 3;
        public const int MinFee = 100000000;
        public const int NftMinFee = 100000;

        public AssetId? AssetId { get; set; } = null!;
        public long Amount { get; set; }
        public string Name { get; set; } = "";
        public long Quantity { get; set; }
        public bool Reissuable { get; set; }
        public int Decimals { get; set; }
        public string Description { get; set; } = "";
        public Base64s? Script { get; set; }

        public IssueTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as IssueTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as IssueTransaction);
        }

        public bool Equals(IssueTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<Base58s?>.Default.Equals(AssetId, other.AssetId) &&
                   Amount == other.Amount &&
                   Name == other.Name &&
                   Quantity == other.Quantity &&
                   Reissuable == other.Reissuable &&
                   Decimals == other.Decimals &&
                   Description == other.Description &&
                   EqualityComparer<Base64s?>.Default.Equals(Script, other.Script);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(AssetId);
            hash.Add(Amount);
            hash.Add(Name);
            hash.Add(Quantity);
            hash.Add(Reissuable);
            hash.Add(Decimals);
            hash.Add(Description);
            hash.Add(Script);
            return hash.ToHashCode();
        }

        public static bool operator ==(IssueTransaction? left, IssueTransaction? right) => EqualityComparer<IssueTransaction>.Default.Equals(left, right);
        public static bool operator !=(IssueTransaction? left, IssueTransaction? right) => !(left == right);
    }
}