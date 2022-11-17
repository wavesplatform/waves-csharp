using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests
{
    [TestClass]
    public class UpdateAssetInfoTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void UpdateAssetInfoTransactionBinarySerializerSuccessTest()
        {
            var tr = UpdateAssetInfoTransactionBuilder.Params(Base58s.Empty, "", "").GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PrivateKey.PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void UpdateAssetInfoTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = UpdateAssetInfoTransactionBuilder.Params(Base58s.Empty, "", "")
                .SetVersion(UpdateAssetInfoTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}