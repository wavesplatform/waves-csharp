using Chaos.NaCl;
using Nethereum.Util;
using org.whispersystems.curve25519;
using System.Security.Cryptography;
using System.Text;
using Waves.NET.Transactions.Common;

using HashAlgorithm = NSec.Cryptography.HashAlgorithm;

namespace Waves.NET.Transactions.Crypto
{
    public static class Crypto
    {
        public static byte[] CalculateBlake2bKeccackHash(byte[] bytes)
        {
            return Sha3Keccack.Current.CalculateHash(HashAlgorithm.Blake2b_256.Hash(bytes));
        }

        public static byte[] CalculateKeccackHash(byte[] bytes)
        {
            return Sha3Keccack.Current.CalculateHash(bytes);
        }

        public static byte[] CalculateBlake2bKeccackSha256Hash(byte[] bytes)
        {
            return HashAlgorithm.Sha256.Hash(CalculateBlake2bKeccackHash(bytes));
        }

        public static byte[] CreatePrivateKeyFromSeed(string seedString, int nonce) => CreatePrivateKeyFromSeed(Encoding.UTF8.GetBytes(seedString), nonce);

        public static byte[] CreatePrivateKeyFromSeed(byte[] seed, int nonce)
        {
            using var ms = new MemoryStream(seed.Length + 4);
            using var bw = new BinaryWriter(ms);
            bw.Write(nonce);
            bw.Write(seed);

            var pk = CalculateBlake2bKeccackSha256Hash(ms.ToArray());
            pk[0] &= 248;
            pk[31] &= 127;
            pk[31] |= 64;

            return pk;
        }

        public static byte[] CreatePublicKeyFromPrivateKey(byte[] privateKey) => MontgomeryCurve25519.GetPublicKey(privateKey);
        public static byte[] CreatePublicKeyFromPrivateKey(string privateKey) => CreatePublicKeyFromPrivateKey(new Base58s(privateKey).Bytes);

        public static byte[] CreateAddressFromPublicKey(byte chainId, byte[] publicKey)
        {
            const int HashSize = 20;

            byte[] hash;

            if (publicKey.Length == PublicKey.ByteLength)
            {
                hash = CalculateBlake2bKeccackHash(publicKey);
            }
            else
            {
                hash = new byte[HashSize];
                Array.Copy(CalculateKeccackHash(publicKey), 12, hash, 0, HashSize);
            }

            using var ms = new MemoryStream(26);
            using var bw = new BinaryWriter(ms);
            bw.Write((byte)1);
            bw.Write(chainId);
            bw.Write(hash, 0, HashSize);

            var checksum = CalculateBlake2bKeccackHash(ms.ToArray());
            bw.Write(checksum, 0, 4);
            return ms.ToArray();
        }

        public static string GenerateRandomSeedPhrase(uint wordsCount = 15)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < wordsCount; i++)
            {
                sb.Append(Bips39English.GetWord(RandomNumberGenerator.GetInt32(Bips39English.MaxIndex + 1)));
                sb.Append(" ");
            }

            return sb.ToString().TrimEnd();
        }

        public static byte[] Sign(PrivateKey privateKey, byte[] message)
        {
            return Curve25519.getInstance(Curve25519.BEST).calculateSignature(privateKey, message);
        }
    }
}
