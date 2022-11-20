using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests
{
    [TestClass]
    public class AddressTests
    {
        public const string PrivatKeyString = "5rRiArQQqc4mR91hfcNhDFneh5e5CC9ntVp27MsoqCAG";
        public const string AddressString = "3N5g9iwdnUj2SFA6buykwzG3URLCBxt9o81";

        public byte[] AddressBytes = null!;

        [TestInitialize]
        public void TestInit()
        {
            WavesConfig.ChainId = ChainIds.TestNet;
            AddressBytes = Base58s.As(AddressString);
        }

        [TestMethod]
        public void CreateAsStringSuccessTest() => AssertAddress(Address.As(AddressString));

        [TestMethod]
        public void CreateAsBytesSuccessTest() => AssertAddress(Address.As(AddressBytes));

        [TestMethod]
        public void CreateCtorStringSuccessTest() => AssertAddress(new Address(AddressString));

        [TestMethod]
        public void CreateCtorBytesSuccessTest() => AssertAddress(new Address(AddressBytes));

        [TestMethod]
        public void AddressesEqualsFailTest()
        {
            var address1 = Address.As(AddressString);
            var address2 = Address.As("3MZpxwxgHhLs8hbS7vdH5caNwpBTVauLnnP");
            Assert.AreNotEqual(address1, address2);
            Assert.IsTrue(address1 != address2);
            Assert.IsFalse(address1.Equals(address2));
            Assert.IsFalse(address1.Equals(null));
        }

        [TestMethod]
        public void AddressesEqualsSuccessTest()
        {
            var address1 = Address.As(AddressString);
            var address2 = Address.As(AddressString);
            Assert.AreEqual(address1, address2);
            Assert.IsTrue(address1 == address2);
            Assert.IsTrue(address1.Equals(address2));
        }

        [TestMethod]
        public void AddressFromPublicKeyTest()
        {
            var publicKey = new PrivateKey(PrivatKeyString).PublicKey;
            var address = Address.FromPublicKey(WavesConfig.ChainId, publicKey);
            AssertAddress(address);
        }

        private void AssertAddress(Address address)
        {
            Assert.IsNotNull(address);
            Assert.AreEqual(1, address.Type);
            Assert.AreEqual(AddressString, address);
            Assert.IsTrue(address.PublicKeyHash.SequenceEqual(address.Bytes[2..22]));
            Assert.AreEqual(WavesConfig.ChainId, address.ChainId);
            Assert.AreEqual(WavesConfig.ChainId, address.Bytes[1]);
        }
    }
}