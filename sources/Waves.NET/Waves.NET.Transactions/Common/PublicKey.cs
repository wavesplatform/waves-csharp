using Newtonsoft.Json;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Transactions.Common
{
    public class PublicKey : Base58s, IEquatable<PublicKey?>
    {
        public const int ByteLength = 32;
        public const int EthBytesLength = 64;

        public PublicKey(byte[] bytes) : base(bytes) { }
        [JsonConstructor]
        public PublicKey(string encoded) : base(encoded) { }

        public override string ToString() => encoded;

        public bool IsZero => bytes is not null && !bytes.Any(x => x != 0);

        public static PublicKey From(PrivateKey privateKey) => new(Crypto.CreatePublicKeyFromPrivateKey(privateKey.Bytes));

        public static PublicKey Zero => As(new byte[ByteLength]);

        public static new PublicKey As(byte[] publicKeyBytes) => new PublicKey(publicKeyBytes);
        public static new PublicKey As(string publicKeyEncoded) => new PublicKey(publicKeyEncoded);

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as PublicKey is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as PublicKey);
        }

        public bool Equals(PublicKey? other) => base.Equals(other);

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode());

        public static implicit operator string(PublicKey x) => x.encoded;
        public static explicit operator PublicKey(string x) => new(x);

        public static bool operator ==(PublicKey? left, PublicKey? right) => EqualityComparer<PublicKey>.Default.Equals(left, right);
        public static bool operator !=(PublicKey? left, PublicKey? right) => !(left == right);
    }
}
