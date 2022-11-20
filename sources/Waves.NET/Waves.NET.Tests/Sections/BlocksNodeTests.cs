using Waves.NET.Blocks.ReturnTypes;
using Waves.NET.Transactions;

namespace Waves.NET.Tests.Sections
{
    [TestClass]
    public class BlocksNodeTests : NodeTestBase
    {
        [TestMethod]
        public void GetBlockHeightTest()
        {
            WaitForHeight(4);
            var blockTimestamp = Node.Blocks.GetBlockHeaders(3).Timestamp;
            Assert.AreEqual(3, Node.Blocks.GetBlockHeight(blockTimestamp));
            Assert.AreEqual(2, Node.Blocks.GetBlockHeight(blockTimestamp - 1));
        }

        [TestMethod]
        public void GetBlockTest()
        {
            var blockIdAtHeight2 = Node.Blocks.GetBlockHeaders(2).Id;
            var headers = Node.Blocks.GetBlockHeaders(2);
            var block = Node.Blocks.GetBlock(2);

            AssertBlockAndHeaderEquality(block, headers);
            Assert.AreEqual(block, Node.Blocks.GetBlock(blockIdAtHeight2));
            Assert.IsTrue(Node.Blocks.GetBlocks(2, 5).Contains(block));
            Assert.AreEqual(headers, Node.Blocks.GetBlockHeaders(blockIdAtHeight2));
            Assert.IsTrue(Node.Blocks.GetBlocks(2, 5).Contains(headers));
        }

        [TestMethod]
        public void GenesisBlockTest()
        {
            var gb = Node.Blocks.GetGenesisBlock();
            var b1 = Node.Blocks.GetBlock(1);
            Assert.AreEqual(gb, b1);
        }

        [TestMethod]
        public void GetLastBlockTest()
        {
            var alice = CreateAccountWithBalance(DataTransaction.MinFee);

            int txHeight = WaitForTransaction(Node.Transactions.Broadcast(
                    DataTransactionBuilder.Params(new [] { new StringEntry { Key = "foo", Value = "bar" } }).GetSignedWith(alice.Pk)).Id).Height;

            var block = Node.Blocks.GetLastBlock();
            var headers = Node.Blocks.GetLastBlockHeaders();

            Assert.AreEqual(block.Height, txHeight);
            AssertBlockAndHeaderEquality(block, headers);
            Assert.IsTrue(block.Fee > 0);
            Assert.IsNotNull(block.Transactions);
            Assert.IsTrue(block.Transactions.Any());
        }

        [TestMethod]
        public void BlocksGeneratedByAddressTest()
        {
            var blocks = Node.Blocks.GetBlocksGeneratedBy(FaucetAddress, 2, 3);

            Assert.AreEqual(2, blocks.Count);
            Assert.IsNotNull(blocks.Contains(Node.Blocks.GetBlock(2)));
            Assert.IsNotNull(blocks.Contains(Node.Blocks.GetBlock(3)));
        }

        [TestMethod]
        public void BlocksDelayTest()
        {
            var blockIdAtStart = Node.Blocks.GetBlockHeaders(Node.Blocks.GetHeight() - 1).Id;
            WaitBlocks(2);

            Assert.IsTrue(Node.Blocks.GetBlocksDelay(blockIdAtStart, 3) > 1000);
        }

        [TestMethod]
        public void BlockTransactionsTest()
        {

            var alice = CreateAccountWithBalance(1000000000);
            var bob = CreateAccountWithBalance(1000000000);

            var transfer = WaitForTransaction(Node.Transactions.Broadcast(
                    TransferTransactionBuilder.Params(bob.Addr, 10).GetSignedWith(alice.Pk)).Id);

            var leasing = WaitForTransaction(Node.Transactions.Broadcast(
                    LeaseTransactionBuilder.Params(bob.Addr, 20).GetSignedWith(alice.Pk)).Id);

            var transferInBlock = Node.Blocks.GetBlock(transfer.Height).Transactions
                .Where(x => x.Transaction.Id == transfer.Transaction.Id).FirstOrDefault();
            Assert.IsNotNull(transferInBlock, "Can't find transfer tx at height " + transfer.Height);

            var leasingInBlock = Node.Blocks.GetBlocks(leasing.Height, leasing.Height + 1).First().Transactions
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
            var leaseTxInfo = WaitForTransaction(Node.Transactions.Broadcast(leaseTx).Id);

            var leaseTxInBlock = Node.Blocks.GetBlock(leaseTxInfo.Height).Transactions
                .Where(x => x.Transaction.Id == leaseTx.Id).FirstOrDefault();
            Assert.IsNotNull(leaseTxInBlock);

            var leaseTxInBlocksSeq = Node.Blocks.GetBlocks(leaseTxInfo.Height, leaseTxInfo.Height).First().Transactions
                .Where(x => x.Transaction.Id == leaseTx.Id).FirstOrDefault();
            Assert.IsNotNull(leaseTxInBlocksSeq);

            Assert.AreEqual(leaseTxInBlock, leaseTxInBlocksSeq);
            Assert.AreEqual(leaseTxInBlock.Transaction.ApplicationStatus, ApplicationStatus.Succeeded);

            // 2. Lease cancel

            var cancelTx = LeaseCancelTransactionBuilder.Params(leaseTx.Id!).GetSignedWith(alice.Pk);
            var cancelTxInfo = WaitForTransaction(Node.Transactions.Broadcast(cancelTx).Id);

            var cancelTxInBlock = Node.Blocks.GetBlock(cancelTxInfo.Height).Transactions
                .Where(x => x.Transaction.Id == cancelTx.Id).FirstOrDefault();
            Assert.IsNotNull(cancelTxInBlock);

            var cancelTxInBlocksSeq = Node.Blocks.GetBlocks(cancelTxInfo.Height, cancelTxInfo.Height).First().Transactions
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