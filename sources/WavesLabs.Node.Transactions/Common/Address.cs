using WavesLabs.Node.Transactions.Utils;

namespace WavesLabs.Node.Transactions.Common
{
    public class Address : Base58s, IRecipient, IEquatable<Address?>
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

        public static implicit operator string(Address x) => x.encoded;
        public static explicit operator Address(string x) => new(x);

        public byte[] PublicKeyHash => Bytes[2..22];

        public override int GetHashCode() => encoded.GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as Address is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as Address);
        }

        public bool Equals(Address? address) =>
            address is not null && (ReferenceEquals(this, address) || encoded.Equals(address.encoded, StringComparison.Ordinal));

        public static bool operator ==(Address a, Address b) => a.Equals(b);
        public static bool operator !=(Address a, Address b) => !a.Equals(b);
    }
}
