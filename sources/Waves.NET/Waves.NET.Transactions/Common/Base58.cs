namespace Waves.NET.Transactions.Common
{
    public class Base58 : ByteString
    {
        public const string Prefix = "base58:";
        public override string EncodedWithPrefix => $"{Prefix}{encoded}";

        public Base58(string encoded)
        {
            bytes = SimpleBase.Base58.Bitcoin.Decode(RemovePrefix(encoded)).ToArray();
        }

        public Base58(byte[] bytes)
        {
            this.bytes = bytes ?? new byte[0];
            encoded = SimpleBase.Base58.Bitcoin.Encode(bytes);
        }

        public static Base58 As(string encoded) => new Base58(encoded);
        public static Base58 As(byte[] bytes) => new Base58(bytes);

        public override string ToString() => encoded;

        public static implicit operator byte[](Base58 x) => x.bytes;
        public static explicit operator Base58(byte[] x) => new(x);

        public static implicit operator string(Base58 x) => x.encoded;
        public static explicit operator Base58(string x) => new(RemovePrefix(x));

        public static string Encode(byte[] bytes) => SimpleBase.Base58.Bitcoin.Encode(bytes);
        public static byte[] Decode(string encoded) => SimpleBase.Base58.Bitcoin.Decode(RemovePrefix(encoded)).ToArray();

        private static string RemovePrefix(string encoded) => (encoded ?? "").Replace(Prefix, "");
    }
}
