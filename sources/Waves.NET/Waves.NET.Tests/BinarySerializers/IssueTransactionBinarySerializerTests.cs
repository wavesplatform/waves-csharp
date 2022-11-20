using Waves.NET.Transactions;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Tests
{
    [TestClass]
    public class IssueTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void IssueTransactionBinarySerializerSuccessTest()
        {
            var tr = IssueTransactionBuilder.Params("", 0, 0).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void IssueTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = IssueTransactionBuilder.Params("", 0, 0).SetVersion(IssueTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}