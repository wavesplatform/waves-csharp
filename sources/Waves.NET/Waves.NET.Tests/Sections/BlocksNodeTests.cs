using Waves.NET.ReturnTypes;
using Waves.NET.Transactions;

namespace Waves.NET.Tests.Sections
{
    [TestClass]
    public class BlocksNodeTests : NodeTestBase
    {
        [TestMethod]
        public void GetBlockHeightTest()
        {
            Node.WaitForHeight(4);
            var header = Node.GetBlocksHeaders(3, 3).FirstOrDefault();
            Assert.IsNotNull(header);
            var blockTimestamp = header.Timestamp;
            Assert.AreEqual(3, Node.GetBlockHeight(blockTimestamp));
            Assert.AreEqual(2, Node.GetBlockHeight(blockTimestamp - 1));
        }

        [TestMethod]
        public void GetBlockTest()
        {
            var blockIdAtHeight2 = Node.GetBlockHeaders(2).Id;
            var headers = Node.GetBlockHeaders(2);
            var block = Node.GetBlock(2);

            AssertBlockAndHeaderEquality(block, headers);
            Assert.AreEqual(block, Node.GetBlock(blockIdAtHeight2));
            Assert.IsTrue(Node.GetBlocks(2, 5).Contains(block));
            Assert.AreEqual(headers, Node.GetBlockHeaders(blockIdAtHeight2));
            Assert.IsTrue(Node.GetBlocks(2, 5).Contains(headers));

            Assert.AreEqual(block.Height, Node.GetBlockHeight(block.Id));
        }

        [TestMethod]
        public void GenesisBlockTest()
        {
            var gb = Node.GetGenesisBlock();
            var b1 = Node.GetBlock(1);
            Assert.AreEqual(gb, b1);
        }

        [TestMethod]
        public void GetLastBlockTest()
        {
            var alice = CreateAccountWithBalance(DataTransaction.MinFee);
            var tx = DataTransactionBuilder.Params(new[] { new StringEntry { Key = "foo", Value = "bar" } }).GetSignedWith(alice.Pk);

            Assert.IsTrue(Node.ValidateTransaction(tx).Valid);

            var txHeight = Node.WaitForTransaction(Node.Broadcast(tx).Id).Height;
            var block = Node.GetLastBlock();
            var headers = Node.GetLastBlockHeaders();

            Assert.AreEqual(block.Height, txHeight);
            AssertBlockAndHeaderEquality(block, headers);
            Assert.IsTrue(block.Fee > 0);
            Assert.IsNotNull(block.Transactions);
            Assert.IsTrue(block.Transactions.Any());
        }

        [TestMethod]
        public void BlocksGeneratedByAddressTest()
        {
            var blocks = Node.GetBlocksGeneratedBy(FaucetAddress, 2, 3);

            Assert.AreEqual(2, blocks.Count);
            Assert.IsNotNull(blocks.Contains(Node.GetBlock(2)));
            Assert.IsNotNull(blocks.Contains(Node.GetBlock(3)));
        }

        [TestMethod]
        public void BlocksDelayTest()
        {
            var blockIdAtStart = Node.GetBlockHeaders(Node.GetHeight() - 1).Id;
            Node.WaitBlocks(2);

            Assert.IsTrue(Node.GetBlocksDelay(blockIdAtStart, 3) > 1000);
        }

        [TestMethod]
        public void BlockTransactionsTest()
        {

            var alice = CreateAccountWithBalance(1000000000);
            var bob = CreateAccountWithBalance(1000000000);

            var transfer = Node.WaitForTransaction(Node.Broadcast(
                    TransferTransactionBuilder.Params(bob.Addr, 10).GetSignedWith(alice.Pk)).Id);

            var leasing = Node.WaitForTransaction(Node.Broadcast(
                    LeaseTransactionBuilder.Params(bob.Addr, 20).GetSignedWith(alice.Pk)).Id);

            var transferInBlock = Node.GetBlock(transfer.Height).Transactions
                .Where(x => x.Transaction.Id == transfer.Transaction.Id).FirstOrDefault();
            Assert.IsNotNull(transferInBlock, "Can't find transfer tx at height " + transfer.Height);

            var leasingInBlock = Node.GetBlocks(leasing.Height, leasing.Height + 1).First().Transactions
                .Where(x => x.Transaction.Id == leasing.Transaction.Id).FirstOrDefault();
            Assert.IsNotNull(leasingInBlock, "Can't find leasing tx at height " + leasing.Height);

            Assert.IsTrue(transferInBlock.Transaction is TransferTransaction);
            Assert.IsTrue(leasingInBlock.Transaction is LeaseTransaction);

            Assert.AreEqual(transferInBlock.Transaction.ApplicationStatus, ApplicationStatus.Succeeded);
            Assert.AreEqual(leasingInBlock.Transaction.ApplicationStatus, ApplicationStatus.Succeeded);

            Assert.AreEqual(((TransferTransaction)transferInBlock.Transaction).Amount, ((TransferTransaction)transfer.Transaction).Amount);
            Assert.AreEqual(((LeaseTransaction)leasingInBlock.Transaction).Amount, ((LeaseTransaction)leasing.Transaction).Amount);
        }

        [TestMethod]
        public void LeasingInBlockTest()
        {
            var alice = CreateAccountWithBalance(1000000);
            var bob = CreateAccountWithBalance(1000000);

            // 1. Lease

            var leaseTx = LeaseTransactionBuilder.Params(bob.Addr, 10000).GetSignedWith(alice.Pk);
            var leaseTxInfo = Node.WaitForTransaction(Node.Broadcast(leaseTx).Id);

            var leaseTxInBlock = Node.GetBlock(leaseTxInfo.Height).Transactions
                .Where(x => x.Transaction.Id == leaseTx.Id).FirstOrDefault();
            Assert.IsNotNull(leaseTxInBlock);

            var leaseTxInBlocksSeq = Node.GetBlocks(leaseTxInfo.Height, leaseTxInfo.Height).First().Transactions
                .Where(x => x.Transaction.Id == leaseTx.Id).FirstOrDefault();
            Assert.IsNotNull(leaseTxInBlocksSeq);

            Assert.AreEqual(leaseTxInBlock, leaseTxInBlocksSeq);
            Assert.AreEqual(leaseTxInBlock.Transaction.ApplicationStatus, ApplicationStatus.Succeeded);

            // 2. Lease cancel

            var cancelTx = LeaseCancelTransactionBuilder.Params(leaseTx.Id!).GetSignedWith(alice.Pk);
            var cancelTxInfo = Node.WaitForTransaction(Node.Broadcast(cancelTx).Id);

            var cancelTxInBlock = Node.GetBlock(cancelTxInfo.Height).Transactions
                .Where(x => x.Transaction.Id == cancelTx.Id).FirstOrDefault();
            Assert.IsNotNull(cancelTxInBlock);

            var cancelTxInBlocksSeq = Node.GetBlocks(cancelTxInfo.Height, cancelTxInfo.Height).First().Transactions
                .Where(x => x.Transaction.Id == cancelTx.Id).FirstOrDefault();
            Assert.IsNotNull(cancelTxInBlocksSeq);

            Assert.AreEqual(cancelTxInBlock, cancelTxInBlocksSeq);
            Assert.AreEqual(cancelTxInBlock.Transaction.ApplicationStatus, ApplicationStatus.Succeeded);
        }

        private void AssertBlockAndHeaderEquality(Block block, BlockHeader headers)
        {
            Assert.AreEqual(block.Id, headers.Id);
            Assert.AreEqual(headers.Id, block.Id);
            Assert.AreEqual(headers.Height, block.Height);
            Assert.AreEqual(headers.NxtConsensus, block.NxtConsensus);
            Assert.AreEqual(headers.DesiredReward, block.DesiredReward);
            Assert.IsTrue(headers.Features.SequenceEqual(block.Features));
            Assert.AreEqual(headers.Generator, block.Generator);
            Assert.AreEqual(headers.Reference, block.Reference);
            Assert.AreEqual(headers.Reward, block.Reward);
            Assert.AreEqual(headers.Signature, block.Signature);
            Assert.AreEqual(headers.Blocksize, block.Blocksize);
            Assert.AreEqual(headers.Timestamp, block.Timestamp);
            Assert.AreEqual(headers.TotalFee, block.TotalFee);
            Assert.AreEqual(headers.TransactionsCount, block.TransactionsCount);
            Assert.AreEqual(headers.TransactionsRoot, block.TransactionsRoot);
            Assert.AreEqual(headers.Version, block.Version);
            Assert.AreEqual(headers.VRF, block.VRF);
        }
    }
}