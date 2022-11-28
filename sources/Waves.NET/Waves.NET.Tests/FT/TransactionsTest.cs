using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Tests.FT
{
    [TestClass]
    public class TransactionsTest : NodeTestBase
    {
        [TestMethod]
        public void SetAssetScriptTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);

            var script = Node.CompileScript("{-# SCRIPT_TYPE ASSET #-} true").Script;
            var assetId = Node.WaitForTransaction<IssueTransactionInfo>(Node.Broadcast(
                    IssueTransactionBuilder.Params("Asset", 1000, 2).SetScript(script).GetSignedWith(alice.Pk)).Id).Transaction.AssetId;

            var tx = SetAssetScriptTransactionBuilder.Params(assetId!, script!).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<SetAssetScriptTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is SetAssetScriptTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SponsorFeeTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);

            var assetId = Node.WaitForTransaction<IssueTransactionInfo>(Node.Broadcast(
                    IssueTransactionBuilder.Params("Asset", 1000, 2).GetSignedWith(alice.Pk)).Id).Transaction.AssetId;

            var tx = SponsorFeeTransactionBuilder.Params(assetId!, 5).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<SponsorFeeTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is SponsorFeeTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SetScriptTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);

            var script = Node.CompileScript("{-# SCRIPT_TYPE ACCOUNT #-} true").Script;
            SetScriptTransaction tx = SetScriptTransactionBuilder.Params(script!).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<SetScriptTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is SetScriptTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DataTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);

            var tx = DataTransactionBuilder.Params(new StringEntry { Key = "str", Value = alice.Addr.ToString() }).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<DataTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is DataTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MassTransferTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            var bob = CreateAccount();

            var tx = MassTransferTransactionBuilder.Params(Transfer.To(bob.Addr, 1000)).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<MassTransferTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is MassTransferTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CreateAliasTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            var alias = Alias.As("alice_" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());

            var tx = CreateAliasTransactionBuilder.Params(alias).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<CreateAliasTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is CreateAliasTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LeaseCancelTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            var bob = CreateAccount();

            var amount = 1000L;

            var leaseTx = LeaseTransactionBuilder.Params(bob.Addr, amount).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(leaseTx).Id);

            var tx = LeaseCancelTransactionBuilder.Params(leaseTx.Id!).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<LeaseCancelTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is LeaseCancelTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LeaseTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            var bob = CreateAccount();

            var tx = LeaseTransactionBuilder.Params(bob.Addr, 1000).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<LeaseTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is LeaseTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
                Assert.AreEqual(txInfo.Status, LeaseStatus.Active);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ExchangeTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            var bob = CreateAccountWithBalance(1000000000);

            var assetId = Node.WaitForTransaction<IssueTransactionInfo>(Node.Broadcast(
                    IssueTransactionBuilder.Params("Asset", 1000, 2).GetSignedWith(alice.Pk)).Id).Transaction.AssetId;

            var amount = 1L;
            var price = 100L;
            var assetPair = new AssetPair { AmountAsset = null, PriceAsset = assetId! };
            var matcherFee = 300000L;
            var buy = OrderBuilder.Params(OrderType.Buy, amount, price, alice.Pk.PublicKey, assetPair).GetSignedWith(alice.Pk);
            var sell = OrderBuilder.Params(OrderType.Sell, amount, price, alice.Pk.PublicKey, assetPair).GetSignedWith(bob.Pk);

            var tx = ExchangeTransactionBuilder.Params(buy, sell, amount, price, matcherFee, matcherFee).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<ExchangeTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is ExchangeTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void BurnTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            var assetId = Node.WaitForTransaction<IssueTransactionInfo>(Node.Broadcast(
                    IssueTransactionBuilder.Params("Asset", 1000, 2).GetSignedWith(alice.Pk)).Id).Transaction.AssetId;

            var tx = BurnTransactionBuilder.Params(assetId!, 100).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<BurnTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is BurnTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ReissueTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);

            var issueTx = Node.Broadcast(IssueTransactionBuilder.Params("Asset", 1000, 2).GetSignedWith(alice.Pk));
            var assetId = Node.WaitForTransaction<IssueTransactionInfo>(issueTx.Id).Transaction.AssetId;

            var reissueTx = ReissueTransactionBuilder.Params(assetId!, 1000).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(reissueTx).Id);

            var commonInfo = Node.GetTransactionInfo(reissueTx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<ReissueTransactionInfo>(reissueTx.Id!);
                Assert.IsTrue(commonInfo.Transaction is ReissueTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TransferTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            var bob = CreateAccount();

            var tx = TransferTransactionBuilder.Params(bob.Addr, 1000).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<TransferTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is TransferTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void IssueTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(10_00000000);
            var tx = IssueTransactionBuilder.Params("Asset", 1000, 2).GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<IssueTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is IssueTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void InvokeScriptTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);
            var bob = CreateAccountWithBalance(1000000000);

            var assetId = Node.WaitForTransaction<IssueTransactionInfo>(Node.Broadcast(
                    IssueTransactionBuilder.Params("Asset", 1000, 2).GetSignedWith(alice.Pk)).Id).Transaction.AssetId!;

            var script = Node.CompileScript(
                    "{-# STDLIB_VERSION 5 #-}\n" +
                    "{-# CONTENT_TYPE DAPP #-}\n" +
                    "{-# SCRIPT_TYPE ACCOUNT #-}\n" +
                    "@Callable(inv)\n" +
                    "func call(bv: ByteVector, b: Boolean, int: Int, str: String, list: List[Int]) = {\n" +
                    "  let asset = Issue(\"Asset\", \"\", 1, 0, true)\n" +
                    "  let assetId = asset.calculateAssetId()\n" +
                    "  let lease = Lease(inv.caller, 7)\n" +
                    "  let leaseId = lease.calculateLeaseId()\n" +
                    "  [\n" +
                    "    BinaryEntry(\"bin\", assetId),\n" +
                    "    BooleanEntry(\"bool\", true),\n" +
                    "    IntegerEntry(\"int\", 100500),\n" +
                    "    StringEntry(\"assetId\", assetId.toBase58String()),\n" +
                    "    StringEntry(\"leaseId\", leaseId.toBase58String()),\n" +
                    "    StringEntry(\"del\", \"\"),\n" +
                    "    DeleteEntry(\"del\"),\n" +
                    "    asset,\n" +
                    "    SponsorFee(assetId, 1),\n" +
                    "    Reissue(assetId, 4, false),\n" +
                    "    Burn(assetId, 3),\n" +
                    "    ScriptTransfer(inv.caller, 2, assetId),\n" +
                    "    lease,\n" +
                    "    LeaseCancel(lease.calculateLeaseId())\n" +
                    "  ]\n" +
                    "}").Script;

            Node.WaitForTransaction(Node.Broadcast(SetScriptTransactionBuilder.Params(script!).GetSignedWith(bob.Pk)).Id);

            var tx = InvokeScriptTransactionBuilder.Params(
                    bob.Addr,
                    new[] {
                        Amount.As(1, assetId),
                        Amount.As(2, assetId),
                        Amount.As(3, assetId),
                        Amount.As(4, assetId),
                        Amount.As(5, assetId),
                        Amount.As(6, assetId),
                        Amount.As(7, assetId),
                        Amount.As(8, assetId),
                        Amount.As(9, assetId),
                        Amount.As(10, assetId)
                    },
                    new Call
                    {
                        Function = "call",
                        Args = new[] {
                            CallArg.AsByteArray(Base64s.From(alice.Addr.Bytes)),
                            CallArg.AsBoolean(true),
                            CallArg.AsInteger(100500L),
                            CallArg.AsString(alice.Addr.ToString()),
                            CallArg.AsList(new List<CallArg> { CallArg.AsInteger(100500L) })
                        }
                    }
                ).SetExtraFee(100000000).GetSignedWith(alice.Pk);

            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<InvokeScriptTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is InvokeScriptTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateAssetInfoTransactionInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);

            var assetId = Node.WaitForTransaction<IssueTransactionInfo>(Node.Broadcast(
                    IssueTransactionBuilder.Params("Asset", 1000, 2).GetSignedWith(alice.Pk)).Id).Transaction.AssetId!;

            Node.WaitBlocks(2);

            var tx = UpdateAssetInfoTransactionBuilder.Params(assetId, "New Asset", "New description").GetSignedWith(alice.Pk);
            Node.WaitForTransaction(Node.Broadcast(tx).Id);

            var commonInfo = Node.GetTransactionInfo(tx.Id!);

            try
            {
                var txInfo = Node.GetTransactionInfo<UpdateAssetInfoTransactionInfo>(tx.Id!);
                Assert.IsTrue(commonInfo.Transaction is UpdateAssetInfoTransaction);
                Assert.AreEqual(commonInfo, txInfo);
                Assert.IsTrue(txInfo.Height > 0);
            }
            catch (InvalidCastException)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MultipleTransactionsInfoTest()
        {
            var alice = CreateAccountWithBalance(1000000000);

            var issueTxId = Node.WaitForTransaction(Node.Broadcast(IssueTransactionBuilder.Params("Asset", 1000, 2)
                .GetSignedWith(alice.Pk)).Id).Transaction.Id!;

            var aliasTxId = Node.WaitForTransaction(Node.Broadcast(
                CreateAliasTransactionBuilder.Params(Alias.As(Node.ChainId, "a" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()))
                .GetSignedWith(alice.Pk)).Id).Transaction.Id!;

            var txsInfo = Node.GetTransactionsInfo(new [] { issueTxId, aliasTxId });

            Assert.AreEqual(2, txsInfo.Count);
            Assert.IsTrue(txsInfo.Any(x => x is IssueTransactionInfo));
            Assert.IsTrue(txsInfo.Any(x => x is CreateAliasTransactionInfo));
        }

        [TestMethod]
        public void MultipleTransactionsInfoWithSpecifiedTypeTest() {
            var alice = CreateAccountWithBalance(1000000000);

            var aliasTxId1 = Node.WaitForTransaction(
                Node.Broadcast(CreateAliasTransactionBuilder.Params(Alias.As(Node.ChainId, "a1" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()))
                .GetSignedWith(alice.Pk)).Id
            ).Transaction.Id!;

            var aliasTxId2 = Node.WaitForTransaction(
                Node.Broadcast(CreateAliasTransactionBuilder.Params(Alias.As(Node.ChainId, "a2" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()))
                .GetSignedWith(alice.Pk)).Id
            ).Transaction.Id!;

            var issueTxId = Node.WaitForTransaction(
                Node.Broadcast(IssueTransactionBuilder.Params("Asset", 1000, 2).GetSignedWith(alice.Pk)).Id
            ).Transaction.Id!;

            var txsInfo = Node.GetTransactionsInfo<CreateAliasTransactionInfo>(new[] { aliasTxId1, aliasTxId2 }).ToList();

            Assert.AreEqual(2, txsInfo.Count);
            CollectionAssert.AllItemsAreInstancesOfType(txsInfo, typeof(CreateAliasTransactionInfo));

            try
            {
                Node.GetTransactionsInfo<CreateAliasTransactionInfo>(new List<Base58s> { aliasTxId1, issueTxId });
                Assert.Fail();
            }
            catch(InvalidCastException) { }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetUtxSizeTest()
        {
            try
            {
                var utxSize = Node.GetUtxSize();
                Assert.IsTrue(utxSize >= 0);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetUnconfirmedTransactionsTest()
        {
            var alice = CreateAccountWithBalance(CreateAliasTransaction.MinFee);

            var txId =  Node.Broadcast(CreateAliasTransactionBuilder
                .Params(Alias.As(Node.ChainId, "c" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())).GetSignedWith(alice.Pk)).Id;

            var utxSize = Node.GetUtxSize();
            var utxs = Node.GetUnconfirmedTransactions();
            var utx = Node.GetUnconfirmedTransaction(txId!);

            Assert.AreEqual(1, utxSize);
            Assert.AreEqual(txId, utx.Id);
            Assert.AreEqual(1, utxs.Count);
        }

        [TestMethod]
        public void GetTransactionsByAddressTest()
        {
            var alice = CreateAccountWithBalance(1000000000);

            var aliasTxId1 = Node.WaitForTransaction(
                Node.Broadcast(CreateAliasTransactionBuilder.Params(Alias.As(Node.ChainId, "a1" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()))
                .GetSignedWith(alice.Pk)).Id
            ).Transaction.Id!;
            Assert.IsTrue(Node.GetTransactionStatus(aliasTxId1).Status.Equals("confirmed", StringComparison.OrdinalIgnoreCase));

            var aliasTxId2 = Node.WaitForTransaction(
                Node.Broadcast(CreateAliasTransactionBuilder.Params(Alias.As(Node.ChainId, "a2" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()))
                .GetSignedWith(alice.Pk)).Id
            ).Transaction.Id!;
            Assert.IsTrue(Node.GetTransactionStatus(aliasTxId2).Status.Equals("confirmed", StringComparison.OrdinalIgnoreCase));

            var issueTxId = Node.WaitForTransaction(
                Node.Broadcast(IssueTransactionBuilder.Params("Asset", 1000, 2).GetSignedWith(alice.Pk), true).Id
            ).Transaction.Id!;
            Assert.IsTrue(Node.GetTransactionStatus(issueTxId).Status.Equals("confirmed", StringComparison.OrdinalIgnoreCase));

            var transactionsPage1 = Node.GetTransactionsByAddress(alice.Addr, 3);
            Assert.AreEqual(3, transactionsPage1.Count);
            Assert.IsTrue(transactionsPage1.Any(x => x.Transaction.Id == issueTxId));
            Assert.IsTrue(transactionsPage1.Any(x => x.Transaction.Id == aliasTxId2));
            Assert.IsTrue(transactionsPage1.Any(x => x.Transaction.Id == aliasTxId1));

            var lastTxId = transactionsPage1.Last().Transaction.Id;
            var transactionsPage2 = Node.GetTransactionsByAddress(alice.Addr, 3, lastTxId);
            Assert.AreEqual(1, transactionsPage2.Count); // the transfer transaction from CreateAccountWithBalance method
            Assert.IsInstanceOfType(transactionsPage2.First(), typeof(TransferTransactionInfo));
        }
    }
}