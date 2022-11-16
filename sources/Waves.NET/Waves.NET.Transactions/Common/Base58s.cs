using Newtonsoft.Json;
using Waves.NET.Transactions.JsonConverters;

namespace Waves.NET.Transactions.Common
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

        private static string RemovePrefix(string encoded) => (encoded ?? "").Replace(Prefix, "");
    }
}
