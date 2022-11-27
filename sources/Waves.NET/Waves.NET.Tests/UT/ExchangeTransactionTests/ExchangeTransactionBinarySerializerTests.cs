using Waves.NET.Transactions;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Tests
{
    [TestClass]
    public class ExchangeTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void ExchangeTransactionBinarySerializerSuccessTest()
        {
            var order = new Order {
                SenderPublicKey = PublicKey,
                MatcherPublicKey = PublicKey, AssetPair = new AssetPair { AmountAsset = null, PriceAsset = null }
            };
            var tr = ExchangeTransactionBuilder.Params(order, order, 0, 0, 0, 0).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ExchangeTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = ExchangeTransactionBuilder.Params(new Order(), new Order(), 0, 0, 0, 0).SetVersion(ExchangeTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}