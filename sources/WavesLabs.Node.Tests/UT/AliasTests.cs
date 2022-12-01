using System.Text;
using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Tests.UT
{
    [TestClass]
    public class AliasTests
    {
        public const string ValidName = "lollypop";
        public const string InvalidName = "!o!!Yp+oP#";

        [TestInitialize]
        public void TestInit()
        {
            WavesConfig.ChainId = ChainIds.TestNet;
        }

        [TestMethod]
        public void CreateSuccessTest()
        {
            var expectedAliasString = $"alias:T:{ValidName}";
            var expectedAliasBytes = new byte[] { 2, WavesConfig.ChainId }
                .Concat(BitConverter.GetBytes((short)ValidName.Length)).Concat(Encoding.UTF8.GetBytes(ValidName)).ToArray();

            var alias = new Alias(ValidName);

            Assert.AreEqual(2, alias.Type);
            Assert.AreEqual(expectedAliasString, alias.ToFullString());
            Assert.AreEqual(ValidName, alias.ToString());
            Assert.AreEqual(ValidName, alias);
            Assert.IsTrue(expectedAliasBytes.SequenceEqual(alias.Bytes));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateFailedTest()
        {
            _ = new Alias(InvalidName);
        }

        [TestMethod]
        public void IsAliasTest()
        {
            Assert.IsTrue(Alias.IsAlias("alias:T:l0lly-pop_be@r.t@i1"));
            Assert.IsFalse(Alias.IsAlias("alias:T:loLLypop"));
            Assert.IsFalse(Alias.IsAlias("l0lly-pop_be@r.t@i1"));
            Assert.IsFalse(Alias.IsAlias("alias:T:l0l"));
            Assert.IsFalse(Alias.IsAlias("alias:T:llllllllllllllllllllllllllllll1"));
        }

        [TestMethod]
        public void AliasesEqualityTest()
        {
            var alias1 = Alias.As(WavesConfig.ChainId, ValidName);
            var alias2 = Alias.As(WavesConfig.ChainId, ValidName);
            Assert.AreEqual(alias1, alias2);
            Assert.IsTrue(alias1 == alias2);

            var alias3 = Alias.As(WavesConfig.ChainId, ValidName + "@");
            Assert.IsTrue(alias1 != alias3);
        }
        [TestMethod]
        public void AddressesEqualsFailTest()
        {
            var alias1 = Alias.As(WavesConfig.ChainId, ValidName);
            var alias2 = Alias.As(WavesConfig.ChainId, ValidName + "@");
            Assert.AreNotEqual(alias1, alias2);
            Assert.IsTrue(alias1 != alias2);
            Assert.IsFalse(alias1.Equals(alias2));
            Assert.IsFalse(alias1.Equals(null));
        }

        [TestMethod]
        public void AddressesEqualsSuccessTest()
        {
            var alias1 = Alias.As(WavesConfig.ChainId, ValidName);
            var alias2 = Alias.As(WavesConfig.ChainId, ValidName);
            Assert.AreEqual(alias1, alias2);
            Assert.IsTrue(alias1 == alias2);
            Assert.IsTrue(alias1.Equals(alias2));
        }
    }
}