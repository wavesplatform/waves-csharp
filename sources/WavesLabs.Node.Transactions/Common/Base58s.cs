using Newtonsoft.Json;
using WavesLabs.Node.Transactions.JsonConverters;

namespace WavesLabs.Node.Transactions.Common
{
    [JsonConverter(typeof(StringJsonConverter))]
    public class Base58s : ByteStr
    {
        public const string Prefix = "base58:";
        public override string EncodedWithPrefix => $"{Prefix}{encoded}";

        public Base58s(string encoded) : this(SimpleBase.Base58.Bitcoin.Decode(RemovePrefix(encoded)).ToArray()) { }

        public Base58s(byte[] bytes)
        {
            this.bytes = bytes ?? new byte[0];
            encoded = SimpleBase.Base58.Bitcoin.Encode(bytes);
        }

        public override string ToString() => encoded;

        public static Base58s As(string encoded) => new Base58s(encoded);
        public static Base58s As(byte[] bytes) => new Base58s(bytes);

        public static Base58s Empty => new Base58s("");

        public static implicit operator byte[](Base58s x) => x.bytes;
        public static explicit operator Base58s(byte[] x) => new(x);

        public static implicit operator string(Base58s x) => x.encoded;
        public static explicit operator Base58s(string x) => new(RemovePrefix(x));

        public static string Encode(byte[] bytes) => SimpleBase.Base58.Bitcoin.Encode(bytes);
        public static byte[] Decode(string encoded) => SimpleBase.Base58.Bitcoin.Decode(RemovePrefix(encoded)).ToArray();

        protected static string RemovePrefix(string encoded) => (encoded ?? "").Replace(Prefix, "");

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as Base58s is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Base58s);
        }

        public bool Equals(Base58s? other) => other is not null && encoded == other.encoded;

        public override int GetHashCode() => HashCode.Combine(encoded);
        public static bool operator ==(Base58s? left, Base58s? right) => EqualityComparer<Base58s>.Default.Equals(left, right);
        public static bool operator !=(Base58s? left, Base58s? right) => !(left == right);
    }
}
