namespace Waves.NET.Transactions.Common
{
    public abstract class ByteString
    {
        protected byte[] bytes = null!;
        protected string encoded = null!;

        public byte[] Bytes => bytes;
        public string Encoded => encoded;

        public abstract string EncodedWithPrefix { get; }
    }
}
