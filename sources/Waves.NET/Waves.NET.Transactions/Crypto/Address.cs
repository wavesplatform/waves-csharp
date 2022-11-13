using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions.Crypto
{
    public class Address : Base58, IRecipient
    {
        public const byte TYPE = 1;

        public byte Type => TYPE;
        public byte ChainId => bytes[1];

        public Address(string encoded) : base(encoded) { }
        public Address(byte[] bytes) : base(bytes) { }

        public static Address FromPublicKey(byte chainId, PublicKey publicKey) =>
            new Address(Crypto.CreateAddressFromPublicKey(chainId, publicKey.Bytes));

        public static new Address As(string encoded) => new Address(encoded);
        public static new Address As(byte[] bytes) => new Address(bytes);

        public byte[] PublicKeyHash => Bytes[2..22];

        public byte[] ToBytes()
        {
            var dataBytes = new byte[] { Type, ChainId }.Concat(PublicKeyHash).ToArray();
            var checksum = Crypto.CalculateBlake2bKeccackHash(dataBytes).Take(4).ToArray();
            return dataBytes.Concat(checksum).ToArray();
        }
    }
}
