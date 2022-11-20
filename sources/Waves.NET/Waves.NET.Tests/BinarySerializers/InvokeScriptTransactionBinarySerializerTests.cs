using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Tests
{
    [TestClass]
    public class InvokeScriptTransactionBinarySerializerTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void InvokeScriptTransactionBinarySerializerSuccessTest()
        {
            var tr = InvokeScriptTransactionBuilder.Params(new Alias("abcd"), new Call()).GetSignedWith(PrivateKey);
            var trBytes = Factory.GetFor(tr).Serialize(tr);
            Assert.IsNotNull(trBytes);
            Assert.IsNotNull(tr.Proofs);
            Assert.IsTrue(tr.Proofs.Count == 1);
            Assert.IsTrue(Crypto.IsProofValid(PublicKey, trBytes, tr.Proofs.Single()));
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void InvokeScriptTransactionBinarySerializerNotSupportedVersionTest()
        {
            var tr = InvokeScriptTransactionBuilder.Params(new Alias("abcd"), new Call())
                .SetVersion(InvokeScriptTransaction.LatestVersion + 1).GetSignedWith(PrivateKey);
            Factory.GetFor(tr).Serialize(tr);
        }
    }
}