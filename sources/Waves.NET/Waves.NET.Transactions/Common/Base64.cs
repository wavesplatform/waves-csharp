namespace Waves.NET.Transactions.Common
{
    public class Base64 : ByteString
    {
        public const string Prefix = "base64:";
        public override string EncodedWithPrefix => $"{Prefix}{encoded}";

        public Base64(string encoded)
        {
            bytes = Convert.FromBase64String(RemovePrefix(encoded));
        }

        public Base64(byte[] bytes)
        {
            this.bytes = bytes ?? new byte[0];
            encoded = Convert.ToBase64String(this.bytes);
        }

        public static Base64 As(string encoded) => new Base64(encoded);
        public static Base64 As(byte[] bytes) => new Base64(bytes);

        public override string ToString() => encoded;

        public static implicit operator byte[](Base64 x) => x.bytes;
        public static explicit operator Base64(byte[] x) => new(x);

        public static implicit operator string(Base64 b) => b.encoded;
        public static explicit operator Base64(string s) => new(RemovePrefix(s));

        public static string Encode(byte[] bytes) => Convert.ToBase64String(bytes);
        public static byte[] Decode(string encoded) => Convert.FromBase64String(RemovePrefix(encoded));

        private static string RemovePrefix(string encoded) => (encoded ?? "").Replace(Prefix, "");
    }
}
