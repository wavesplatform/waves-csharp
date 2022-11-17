using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests
{
    [TestClass]
    public class ReissueTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void ReissueTransactionBinarySerializerSuccessTest()
        {
            var tr = ReissueTransactionBuilder.Params(Base58s.Empty, 0, false).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PrivateKey.PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ReissueTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = ReissueTransactionBuilder.Params(Base58s.Empty, 0, false).SetVersion(ReissueTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}