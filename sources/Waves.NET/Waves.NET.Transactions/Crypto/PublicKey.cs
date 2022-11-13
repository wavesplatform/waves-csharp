using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions.Crypto
{
    public class PublicKey : Base58
    {
        public const int ByteLength = 32;
        public const int EthBytesLength = 64;

        protected PublicKey(byte[] bytes) : base(bytes) { }
        protected PublicKey(string encoded) : base(encoded) { }

        public override string ToString() => encoded;

        public bool IsZero => bytes is not null && !bytes.Any(x => x != 0);

        public static PublicKey From(PrivateKey privateKey) => new(Crypto.CreatePublicKeyFromPrivateKey(privateKey.Bytes));
        public static PublicKey Zero => As(new byte[ByteLength]);
        public static new PublicKey As(byte[] publicKeyBytes) => new PublicKey(publicKeyBytes);
        public static new PublicKey As(string publicKeyEncoded) => new PublicKey(publicKeyEncoded);
    }
}
