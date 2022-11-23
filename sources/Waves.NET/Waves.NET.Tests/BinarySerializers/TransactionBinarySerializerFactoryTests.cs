using Waves.NET.Transactions;
using Waves.NET.Transactions.TransactionData;

namespace Waves.NET.Tests
{
    [TestClass]
    public class TransactionBinarySerializerFactoryTests : TransactionBinarySerializerTestsBase
    {
        [TestMethod]
        public void SerializersCountTest()
        {
            Assert.AreEqual(16, Factory.CerializersCount);
        }

        [TestMethod]
        public void GetTest()
        {
            Assert.IsTrue(Factory.Get(TransactionType.Genesis) is GenesisTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.Issue) is IssueTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.Transfer) is TransferTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.Reissue) is ReissueTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.Burn) is BurnTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.Exchange) is ExchangeTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.Lease) is LeaseTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.LeaseCancel) is LeaseCancelTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.CreateAlias) is CreateAliasTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.MassTransfer) is MassTransferTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.Data) is DataTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.SetScript) is SetScriptTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.SponsorFee) is SponsorFeeTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.SetAssetScript) is SetAssetScriptTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.InvokeScript) is InvokeScriptTransactionBinarySerializer);
            Assert.IsTrue(Factory.Get(TransactionType.UpdateAssetInfo) is UpdateAssetInfoTransactionBinarySerializer);
        }

        [TestMethod]
        public void GetForTest()
        {
            Assert.IsTrue(Factory.GetFor(new GenesisTransaction()) is GenesisTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new IssueTransaction()) is IssueTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new TransferTransaction()) is TransferTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new ReissueTransaction()) is ReissueTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new BurnTransaction()) is BurnTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new ExchangeTransaction()) is ExchangeTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new LeaseTransaction()) is LeaseTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new LeaseCancelTransaction()) is LeaseCancelTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new CreateAliasTransaction()) is CreateAliasTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new MassTransferTransaction()) is MassTransferTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new DataTransaction()) is DataTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new SetScriptTransaction()) is SetScriptTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new SponsorFeeTransaction()) is SponsorFeeTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new SetAssetScriptTransaction()) is SetAssetScriptTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new InvokeScriptTransaction()) is InvokeScriptTransactionBinarySerializer);
            Assert.IsTrue(Factory.GetFor(new UpdateAssetInfoTransaction()) is UpdateAssetInfoTransactionBinarySerializer);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void NotSupportedTransactionTypeTest()
        {
            Factory.Get(TransactionType.Unknown);
        }
    }
}