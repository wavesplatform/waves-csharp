using SimpleBase;

namespace Waves.NET.Transactions.Common
{
    public class Base58String : ByteString
    {
        public override string EncodedWithPrefix => $"base58:{encoded}";

        public Base58String(string encoded)
        {
            this.encoded = encoded ?? "";
            bytes = Base58.Bitcoin.Decode(this.encoded).ToArray();
        }

        public Base58String(byte[] bytes)
        {
            this.bytes = bytes ?? new byte[0];
            encoded = Base58.Bitcoin.Encode(bytes);
        }

        public override string ToString() => encoded;

        public static implicit operator byte[](Base58String x) => x.bytes;
        public static explicit operator Base58String(byte[] x) => new(x);

        public static implicit operator string(Base58String x) => x.encoded;
        public static explicit operator Base58String(string x) => new(x);

        public static string Encode(byte[] bytes) => Base58.Bitcoin.Encode(bytes);
        public static byte[] Decode(string encoded) => Base58.Bitcoin.Decode(encoded).ToArray();
    }
}
