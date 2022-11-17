using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests
{
    [TestClass]
    public class CreateAliasTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void CreateAliasTransactionBinarySerializerSuccessTest()
        {
            var tr = CreateAliasTransactionBuilder.Params(Alias.As("abcd")).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PrivateKey.PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void CreateAliasTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = CreateAliasTransactionBuilder.Params(Alias.As("abcd")).SetVersion(CreateAliasTransaction.LatestVersion+1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}