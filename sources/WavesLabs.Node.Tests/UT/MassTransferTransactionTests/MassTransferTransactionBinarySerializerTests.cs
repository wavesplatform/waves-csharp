using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions.Utils;

namespace WavesLabs.Node.Tests.UT
{
    [TestClass]
    public class MassTransferTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void MassTransferTransactionBinarySerializerSuccessTest()
        {
            var tr = MassTransferTransactionBuilder.Params(new List<Transfer>(), AssetId.Waves, Base58s.Empty).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void MassTransferTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = MassTransferTransactionBuilder.Params(new List<Transfer>(), AssetId.Waves, Base58s.Empty)
                .SetVersion(MassTransferTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}