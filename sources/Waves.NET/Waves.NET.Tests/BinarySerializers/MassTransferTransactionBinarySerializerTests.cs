using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests
{
    [TestClass]
    public class MassTransferTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void MassTransferTransactionBinarySerializerSuccessTest()
        {
            var tr = MassTransferTransactionBuilder.Params(Base58s.Empty, Base58s.Empty, 0, 0, new List<Transfer>()).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PrivateKey.PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void MassTransferTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = MassTransferTransactionBuilder.Params(Base58s.Empty, Base58s.Empty, 0, 0, new List<Transfer>())
                .SetVersion(MassTransferTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}