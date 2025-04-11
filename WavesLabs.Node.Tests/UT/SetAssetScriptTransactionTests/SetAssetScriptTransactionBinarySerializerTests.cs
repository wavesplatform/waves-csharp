using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions.Utils;

namespace WavesLabs.Node.Tests.UT
{
    [TestClass]
    public class SetAssetScriptTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void SetAssetScriptTransactionBinarySerializerSuccessTest()
        {
            var tr = SetAssetScriptTransactionBuilder.Params(AssetId.Waves, "").GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void SetAssetScriptTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = SetAssetScriptTransactionBuilder.Params(AssetId.Waves, "").SetVersion(SetAssetScriptTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}