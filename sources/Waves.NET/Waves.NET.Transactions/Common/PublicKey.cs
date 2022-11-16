using Newtonsoft.Json;

namespace Waves.NET.Transactions.Common
{
    public class PublicKey : Base58s
    {
        public const int ByteLength = 32;
        public const int EthBytesLength = 64;

        public PublicKey(byte[] bytes) : base(bytes) { }
        [JsonConstructor]
        public PublicKey(string encoded) : base(encoded) { }

        public override string ToString() => encoded;

        public bool IsZero => bytes is not null && !bytes.Any(x => x != 0);

        public static PublicKey From(PrivateKey privateKey) => new(Crypto.CreatePublicKeyFromPrivateKey(privateKey.Bytes));

        public static PublicKey Zero => As(new byte[ByteLength]);

        public static new PublicKey As(byte[] publicKeyBytes) => new PublicKey(publicKeyBytes);
        public static new PublicKey As(string publicKeyEncoded) => new PublicKey(publicKeyEncoded);

        public static implicit operator string(PublicKey x) => x.encoded;
        public static explicit operator PublicKey(string x) => new(x);
    }
}
