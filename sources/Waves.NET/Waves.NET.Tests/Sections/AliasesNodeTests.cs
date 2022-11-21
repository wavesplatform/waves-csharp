using Waves.NET.Tests.Sections;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests.Sections
{
    [TestClass]
    public class AliasesNodeTests : NodeTestBase
    {
        [TestMethod]
        public void AliasTest()
        {
            var alias = Alias.As(Node.ChainId, $"a{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");
            var alice = CreateAccountWithBalance(CreateAliasTransaction.MinFee);

            WaitForTransaction(Node.Transactions.Broadcast(CreateAliasTransactionBuilder.Params(alias).GetSignedWith(alice.Pk)).Id);

            var aliases = Node.Aliases.GetAliasesByAddress(alice.Addr);
            Assert.IsNotNull(aliases);
            Assert.IsNotNull(aliases.FirstOrDefault());
            Assert.AreEqual(alias, aliases.FirstOrDefault());

            var result = Node.Aliases.GetAddressByAlias(alias);
            Assert.IsNotNull(result);
            Assert.AreEqual(alice.Addr, result);
        }
    }
}