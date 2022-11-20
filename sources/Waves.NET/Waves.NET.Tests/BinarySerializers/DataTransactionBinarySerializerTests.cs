using Waves.NET.Transactions;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Tests
{
    [TestClass]
    public class DataTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void DataTransactionBinarySerializerSuccessTest()
        {
            var data = new List<EntryData> { new BooleanEntry { Key = "k", Value = true } };
            var tr = DataTransactionBuilder.Params(data).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void DataTransactionBinarySerializerNotSupportedVersionTest()
        {
            var data = new List<EntryData> { new BooleanEntry { Key = "k", Value = true } };
            var tr = DataTransactionBuilder.Params(data).SetVersion(DataTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}