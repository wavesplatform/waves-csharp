using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests.FT
{
    [TestClass]
    public class AliasesNodeTests : NodeTestBase
    {
        [TestMethod]
        public void AliasTest()
        {
            var alias = Alias.As(Node.ChainId, $"a{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}");
            var alice = CreateAccountWithBalance(CreateAliasTransaction.MinFee);

            Node.WaitForTransaction(Node.Broadcast(CreateAliasTransactionBuilder.Params(alias).GetSignedWith(alice.Pk)).Id);

            var aliases = Node.GetAliasesByAddress(alice.Addr);
            Assert.IsNotNull(aliases);
            Assert.IsNotNull(aliases.FirstOrDefault());
            Assert.AreEqual(alias, aliases.FirstOrDefault());

            var result = Node.GetAddressByAlias(alias);
            Assert.IsNotNull(result);
            Assert.AreEqual(alice.Addr, result);
        }
    }
}