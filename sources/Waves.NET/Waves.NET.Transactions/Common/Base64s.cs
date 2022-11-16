using Newtonsoft.Json;
using Waves.NET.Transactions.JsonConverters;

namespace Waves.NET.Transactions.Common
{
    [JsonConverter(typeof(StringJsonConverter))]
    public class Base64s : ByteStr
    {
        public const string Prefix = "base64:";
        public override string EncodedWithPrefix => $"{Prefix}{encoded}";

        public Base64s(string encoded)
        {
            bytes = Convert.FromBase64String(RemovePrefix(encoded));
        }

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

        public static string Encode(byte[] bytes) => Convert.ToBase64String(bytes);
        public static byte[] Decode(string encoded) => Convert.FromBase64String(RemovePrefix(encoded));

        private static string RemovePrefix(string encoded) => (encoded ?? "").Replace(Prefix, "");
    }
}
