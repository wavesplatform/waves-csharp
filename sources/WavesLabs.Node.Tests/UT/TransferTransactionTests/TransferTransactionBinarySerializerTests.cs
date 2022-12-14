using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions.Utils;

namespace WavesLabs.Node.Tests.UT
{
    [TestClass]
    public class TransferTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void TransferTransactionBinarySerializerSuccessTest()
        {
            var tr = TransferTransactionBuilder.Params(Alias.As("abcd"), 0).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PrivateKey.PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void TransferTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = TransferTransactionBuilder.Params(Alias.As("abcd"), 0).SetVersion(TransferTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}