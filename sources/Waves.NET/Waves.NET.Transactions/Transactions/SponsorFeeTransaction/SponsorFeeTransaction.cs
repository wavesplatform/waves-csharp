using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class SponsorFeeTransaction : Transaction, ISponsorFeeTransaction, IEquatable<SponsorFeeTransaction?>
    {
        public const int TYPE = 14;
        public const int LatestVersion = 2;
        public const int MinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public long MinSponsoredAssetFee { get; set; }

        public SponsorFeeTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as SponsorFeeTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as SponsorFeeTransaction);
        }

        public bool Equals(SponsorFeeTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<Base58s?>.Default.Equals(AssetId, other.AssetId) &&
                   MinSponsoredAssetFee == other.MinSponsoredAssetFee;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), AssetId, MinSponsoredAssetFee);
        public static bool operator ==(SponsorFeeTransaction? left, SponsorFeeTransaction? right) => EqualityComparer<SponsorFeeTransaction>.Default.Equals(left, right);
        public static bool operator !=(SponsorFeeTransaction? left, SponsorFeeTransaction? right) => !(left == right);
    }
}