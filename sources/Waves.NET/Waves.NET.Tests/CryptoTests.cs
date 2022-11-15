using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Tests
{
    [TestClass]
    public class CryptoTests
    {
        private const string SeedPhrase = "manage manual recall harvest series desert melt police rose hollow moral pledge kitten position add";

        [TestMethod]
        public void PrivateAndPublicKeysGenearationTest()
        {
            var privateKey = Crypto.CreatePrivateKeyFromSeed(SeedPhrase, 0);
            var publicKey = Crypto.CreatePublicKeyFromPrivateKey(new Base58s(privateKey).ToString());
            Assert.AreEqual(32, privateKey.Length);
            Assert.AreEqual(32, publicKey.Length);
            Assert.AreEqual("3kMEhU5z3v8bmer1ERFUUhW58Dtuhyo9hE5vrhjqAWYT", new Base58s(privateKey).ToString());
            Assert.AreEqual("HBqhfdFASRQ5eBBpu2y6c6KKi1az6bMx8v1JxX4iW1Q8", new Base58s(publicKey).ToString());
        }

        [TestMethod]
        public void AddressFromPublicKeyGenerationTest()
        {
            var privateKey = Crypto.CreatePrivateKeyFromSeed(SeedPhrase, 0);
            var publicKey = Crypto.CreatePublicKeyFromPrivateKey(privateKey);
            var address = Crypto.CreateAddressFromPublicKey(ChainIds.MainNet, publicKey);
            Assert.AreEqual(26, address.Length);
            Assert.AreEqual("3PPbMwqLtwBGcJrTA5whqJfY95GqnNnFMDX", new Base58s(address).ToString());
        }

        [TestMethod]
        public void GenerateRandomSeedPhraseTest()
        {
            const int wordsCount = 15;
            var seedPhrase = Crypto.GenerateRandomSeedPhrase(wordsCount);
            Assert.IsNotNull(seedPhrase);
            Assert.IsTrue(seedPhrase.Length > 0);

            var splitted = seedPhrase.Split(' ');
            Assert.AreEqual(wordsCount, splitted.Length);

        }
    }
}