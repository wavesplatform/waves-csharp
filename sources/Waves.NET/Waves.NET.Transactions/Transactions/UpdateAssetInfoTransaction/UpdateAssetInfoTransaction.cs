using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class UpdateAssetInfoTransaction : Transaction, IUpdateAssetInfoTransaction, IEquatable<UpdateAssetInfoTransaction?>
    {
        public const int TYPE = 17;
        public const int LatestVersion = 1;
        public const int MinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public UpdateAssetInfoTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as UpdateAssetInfoTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as UpdateAssetInfoTransaction);
        }

        public bool Equals(UpdateAssetInfoTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<Base58s?>.Default.Equals(AssetId, other.AssetId) &&
                   Name == other.Name &&
                   Description == other.Description;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), AssetId, Name, Description);
        public static bool operator ==(UpdateAssetInfoTransaction? left, UpdateAssetInfoTransaction? right) =>
            EqualityComparer<UpdateAssetInfoTransaction>.Default.Equals(left, right);
        public static bool operator !=(UpdateAssetInfoTransaction? left, UpdateAssetInfoTransaction? right) => !(left == right);
    }
}