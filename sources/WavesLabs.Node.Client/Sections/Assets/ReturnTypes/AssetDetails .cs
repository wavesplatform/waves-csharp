using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Client.ReturnTypes
{
    public class AssetDetails : IEquatable<AssetDetails?>
    {
        public Base58s? AssetId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Decimals { get; set; }
        public int IssueHeight { get; set; }
        public long IssueTimestamp { get; set; }
        public Address Issuer { get; set; } = null!;
        public PublicKey IssuerPublicKey { get; set; } = null!;
        public bool Reissuable { get; set; }
        public bool Scripted { get; set; }
        public long? MinSponsoredAssetFee { get; set; }
        public string OriginTransactionId { get; set; } = null!;
        public ScriptDetails? ScriptDetails { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as AssetDetails is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as AssetDetails);
        }

        public bool Equals(AssetDetails? other)
        {
            return other is not null &&
                AssetId == other.AssetId &&
                Name == other.Name &&
                Description == other.Description &&
                Decimals == other.Decimals &&
                IssueHeight == other.IssueHeight &&
                IssueTimestamp == other.IssueTimestamp &&
                Issuer == other.Issuer &&
                IssuerPublicKey == other.IssuerPublicKey &&
                Reissuable == other.Reissuable &&
                Scripted == other.Scripted &&
                MinSponsoredAssetFee == other.MinSponsoredAssetFee &&
                OriginTransactionId == other.OriginTransactionId &&
                ScriptDetails == other.ScriptDetails;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(AssetId);
            hash.Add(Name);
            hash.Add(Description);
            hash.Add(Decimals);
            hash.Add(IssueHeight);
            hash.Add(IssueTimestamp);
            hash.Add(Issuer);
            hash.Add(IssuerPublicKey);
            hash.Add(Reissuable);
            hash.Add(Scripted);
            hash.Add(MinSponsoredAssetFee);
            hash.Add(OriginTransactionId);
            hash.Add(ScriptDetails);
            return hash.ToHashCode();
        }

        public static bool operator ==(AssetDetails? left, AssetDetails? right) => left == right;
        public static bool operator !=(AssetDetails? left, AssetDetails? right) => !(left == right);
    }
}
