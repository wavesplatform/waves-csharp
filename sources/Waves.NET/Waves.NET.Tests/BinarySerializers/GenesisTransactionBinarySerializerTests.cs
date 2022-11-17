using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests
{
    [TestClass]
    public class GenesisTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void GenesisTransactionBinarySerializerSuccessTest()
        {
            var address = Address.FromPublicKey(ChainIds.TestNet, PrivateKey.PublicKey);
            var tr = GenesisTransactionBuilder.Params(address, 0).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PrivateKey.PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GenesisTransactionBinarySerializerNotSupportedVersionTest()
        {
            var address = Address.FromPublicKey(ChainIds.TestNet, PrivateKey.PublicKey);
            var tr = GenesisTransactionBuilder.Params(address, 0).SetVersion(GenesisTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}