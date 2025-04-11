namespace WavesLabs.Node.Transactions.Common
{
    public class AssetId : Base58s, IEquatable<AssetId?>
    {
        public static AssetId Waves => new ("");

        public AssetId(string encoded) : base(encoded) { }

        public AssetId(byte[] bytes) : base(bytes) { }

        public static new AssetId As(string encoded) => new (encoded);
        public static new AssetId As(byte[] bytes) => new (bytes);

        public static implicit operator byte[](AssetId x) => x.bytes;
        public static explicit operator AssetId(byte[] x) => new(x);

        public static implicit operator string(AssetId x) => x.encoded;
        public static explicit operator AssetId(string x) => new(RemovePrefix(x));

        public override string ToString() => base.ToString();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as AssetId is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as AssetId);
        }

        public bool Equals(AssetId? other) => other is not null && base.Equals(other);

        public override int GetHashCode() => base.GetHashCode();

        public static bool operator ==(AssetId? left, AssetId? right) => EqualityComparer<AssetId>.Default.Equals(left, right);

        public static bool operator !=(AssetId? left, AssetId? right) => !(left == right);
    }
}
