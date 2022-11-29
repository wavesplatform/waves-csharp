using System.Text;
using WavesLabs.Node.Transactions.Utils;

namespace WavesLabs.Node.Transactions.Common
{
    public class PrivateKey : Base58s
    {
        public static int Length = 32;

        public PublicKey PublicKey { get; init; }

        public static PrivateKey FromSeed(string seedPhrase, int nonce = 0) => FromSeed(Encoding.UTF8.GetBytes(seedPhrase), nonce);

        public static PrivateKey FromSeed(byte[] seedPhraseBytes, int nonce = 0) => new(Crypto.CreatePrivateKeyFromSeed(seedPhraseBytes, nonce));

        public PrivateKey(string base58Encoded) : base(base58Encoded)
        {
            if (bytes.Length != Length)
            {
                throw new ArgumentOutOfRangeException($"Private key has wrong size in bytes. Expected: {Length}, actual: {bytes.Length}");
            }

            PublicKey = PublicKey.From(this);
        }

        public PrivateKey(byte[] privateKey) : base(privateKey)
        {
            if (privateKey.Length != Length)
            {
                throw new ArgumentOutOfRangeException($"Private key has wrong size in bytes. Expected: {Length}, actual: {privateKey.Length}");
            }

            PublicKey = PublicKey.From(this);
        }

        public byte[] Sign(byte[] message)
        {
            return Crypto.Sign(this, message);
        }
    }
}
