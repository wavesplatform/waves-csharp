using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions.Crypto
{
    public class Address : Base58String
    {
        public Address(string encoded) : base(encoded) { }
        public Address(byte[] bytes) : base(bytes) { }

        public static Address FromPublicKey(PublicKey publicKey, byte chainId) =>
            new Address(Crypto.CreateAddressFromPublicKey(publicKey.Bytes, chainId));
    }
}
