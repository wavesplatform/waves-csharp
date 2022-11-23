using System.Text;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Tests.Sections
{
    [TestClass]
    public class UtilsNodeTests : NodeTestBase
    {
        private const string MessageToHash = "40000 monkeys and bananas";

        [TestMethod]
        public void GenerateRandomSeedTest()
        {
            var seed = Node.GenerateRandomSeed();
            Assert.IsNotNull(seed);
            Assert.IsTrue(seed.Length > 0);
        }

        [TestMethod]
        public void GenerateRandomSeedOfLengthTest()
        {
            var seed = Node.GenerateRandomSeedOfLength(64);
            Assert.IsNotNull(seed);
            Assert.AreEqual(64, Base58s.Decode(seed).Length);
        }

        [TestMethod]
        public void GetNodeTimeUtcTest()
        {
            var nt = Node.GetNodeTimeUtc();
            Assert.IsNotNull(nt);
            Assert.IsTrue(nt.System > 0);
            Assert.IsTrue(nt.Ntp > 0);
        }

        [TestMethod]
        public void GetFastHashTest()
        {
            var expectedHash = Base58s.As(Crypto.CalculateBlake2bHash(Encoding.UTF8.GetBytes(MessageToHash)));
            var hash = Node.GetFastHash(MessageToHash);
            Assert.IsNotNull(hash);
            Assert.AreEqual(expectedHash, hash);
        }

        [TestMethod]
        public void GetSecureHashTest()
        {
            var expectedHash = Base58s.As(Crypto.CalculateBlake2bKeccackHash(Encoding.UTF8.GetBytes(MessageToHash)));
            var hash = Node.GetSecureHash(MessageToHash);
            Assert.IsNotNull(hash);
            Assert.AreEqual(expectedHash, hash);
        }
    }
}