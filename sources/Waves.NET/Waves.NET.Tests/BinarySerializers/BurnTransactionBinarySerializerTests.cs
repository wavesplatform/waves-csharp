using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests
{
    [TestClass]
    public class BurnTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void BurnTransactionBinarySerializerSuccessTest()
        {
            var tr = BurnTransactionBuilder.Params(Base58s.Empty, 0).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PrivateKey.PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void BurnTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = BurnTransactionBuilder.Params(Base58s.Empty, 0).SetVersion(BurnTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}