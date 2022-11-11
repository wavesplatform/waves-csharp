namespace Waves.NET.Transactions.Common
{
    public class Base64String : ByteString
    {
        public override string EncodedWithPrefix => $"base64:{encoded}";

        public Base64String(string encoded)
        {
            this.encoded = encoded ?? "";
            bytes = Convert.FromBase64String(this.encoded);
        }

        public Base64String(byte[] bytes)
        {
            this.bytes = bytes ?? new byte[0];
            encoded = Convert.ToBase64String(this.bytes);
        }

        public override string ToString() => encoded;

        public static implicit operator string(Base64String b) => b.encoded;
        public static explicit operator Base64String(string s) => new(s);
    }
}
