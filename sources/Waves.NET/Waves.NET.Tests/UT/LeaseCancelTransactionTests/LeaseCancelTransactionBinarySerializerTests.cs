using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Tests.UT
{
    [TestClass]
    public class LeaseCancelTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void LeaseCancelTransactionBinarySerializerSuccessTest()
        {
            var tr = LeaseCancelTransactionBuilder.Params(Base58s.Empty).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void LeaseCancelTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = LeaseCancelTransactionBuilder.Params(Base58s.Empty)
                .SetVersion(LeaseCancelTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}