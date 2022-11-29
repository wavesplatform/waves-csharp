using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class SetAssetScriptTransaction : Transaction, ISetAssetScriptTransaction, IEquatable<SetAssetScriptTransaction?>
    {
        public const int TYPE = 15;
        public const int LatestVersion = 2;
        public const int MinFee = 100000000;

        public Base58s? AssetId { get; set; } = null!;
        public string Script { get; set; } = null!;

        public SetAssetScriptTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as SetAssetScriptTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as SetAssetScriptTransaction);
        }

        public bool Equals(SetAssetScriptTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<Base58s?>.Default.Equals(AssetId, other.AssetId) &&
                   Script == other.Script;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), AssetId, Script);
        public static bool operator ==(SetAssetScriptTransaction? left, SetAssetScriptTransaction? right) =>
            EqualityComparer<SetAssetScriptTransaction>.Default.Equals(left, right);
        public static bool operator !=(SetAssetScriptTransaction? left, SetAssetScriptTransaction? right) => !(left == right);
    }
}