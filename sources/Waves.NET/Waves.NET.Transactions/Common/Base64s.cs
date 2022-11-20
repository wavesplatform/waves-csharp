using Newtonsoft.Json;
using System.Text;
using Waves.NET.Transactions.JsonConverters;

namespace Waves.NET.Transactions.Common
{
    [JsonConverter(typeof(StringJsonConverter))]
    public class Base64s : ByteStr, IEquatable<Base64s?>
    {
        public const string Prefix = "base64:";
        public override string EncodedWithPrefix => $"{Prefix}{encoded}";

        public Base64s(string encoded) : this(Convert.FromBase64String(RemovePrefix(encoded))) { }
        public Base64s(byte[] bytes)
        {
            this.bytes = bytes ?? new byte[0];
            encoded = Convert.ToBase64String(this.bytes);
        }

        public static Base64s As(string encoded) => new Base64s(encoded);
        public static Base64s As(byte[] bytes) => new Base64s(bytes);

        public override string ToString() => encoded;

        public static implicit operator byte[](Base64s x) => x.bytes;
        public static explicit operator Base64s(byte[] x) => new(x);

        public static implicit operator string(Base64s b) => b.encoded;
        public static explicit operator Base64s(string s) => new(RemovePrefix(s));

        public static Base64s From(string str) => new Base64s(Convert.ToBase64String(Encoding.UTF8.GetBytes(str)));
        public static byte[] Decode(string encoded) => Convert.FromBase64String(RemovePrefix(encoded));

        private static string RemovePrefix(string encoded) => (encoded ?? "").Replace(Prefix, "");

        public static bool operator ==(Base64s? left, Base64s? right) => EqualityComparer<Base64s>.Default.Equals(left, right);
        public static bool operator !=(Base64s? left, Base64s? right) => !(left == right);

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as Base64s is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Base64s);
        }

        public bool Equals(Base64s? other) => other is not null && encoded == other.encoded;

        public override int GetHashCode() => HashCode.Combine(encoded);
    }
}
