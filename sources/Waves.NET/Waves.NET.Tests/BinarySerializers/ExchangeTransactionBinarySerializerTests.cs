using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests
{
    [TestClass]
    public class ExchangeTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void ExchangeTransactionBinarySerializerSuccessTest()
        {
            var order = new Order {
                SenderPublicKey = PrivateKey.PublicKey,
                MatcherPublicKey = PrivateKey.PublicKey, AssetPair = new AssetPair { AmountAsset = "", PriceAsset = "" }
            };
            var tr = ExchangeTransactionBuilder.Params(order, order, 0, 0, 0, 0).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PrivateKey.PublicKey, trBytes, tr.Proofs.Single()));
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