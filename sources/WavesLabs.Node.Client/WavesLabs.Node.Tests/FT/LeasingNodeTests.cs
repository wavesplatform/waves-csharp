using WavesLabs.Node.Client.Transactions;
using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Tests.FT
{
    [TestClass]
    public class LeasingNodeTests : NodeTestBase
    {
        [TestMethod]
        public void LeaseInfoTest()
        {
            var alice = CreateAccountWithBalance(10000000);
            var bob = CreateAccountWithBalance(10000000);

            var dAppScript = Node.CompileScript(
                "{-# STDLIB_VERSION 5 #-}\n{-# CONTENT_TYPE DAPP #-}\n{-# SCRIPT_TYPE ACCOUNT #-}\n" +
                        "@Callable(inv)\nfunc lease(amount: Int) = [Lease(inv.caller, amount)]\n" +
                        "@Callable(inv)\nfunc cancel(leaseId: String) = [LeaseCancel(leaseId.fromBase58String())]").Script;

            Node.WaitForTransaction(Node.Broadcast(SetScriptTransactionBuilder.Params(dAppScript!).GetSignedWith(bob.Pk)).Id);

            // 1. Send leasing

            var leaseAmount = 10000L;
            var invokeLeaseAmount = 20000L;

            var leaseTx = Node.WaitForTransaction(Node.Broadcast(
                LeaseTransactionBuilder.Params(bob.Addr, leaseAmount).GetSignedWith(alice.Pk)).Id);

            var invokeTx = Node.WaitForTransaction(Node.Broadcast(
                    InvokeScriptTransactionBuilder.Params(
                        bob.Addr,
                        new Call {
                            Function = "lease",
                            Args = new List<CallArg> { CallArg.AsInteger(invokeLeaseAmount) }
                        }
                    ).GetSignedWith(alice.Pk)).Id);

            var txInfo = Node.GetTransactionsInfo(new[] { invokeTx.Transaction.Id! }).FirstOrDefault() as InvokeScriptTransactionInfo;
            Assert.IsNotNull(txInfo);

            txInfo = Node.GetTransactionInfo(invokeTx.Transaction.Id!) as InvokeScriptTransactionInfo;
            Assert.IsNotNull(txInfo);

            var stateChangesLease = txInfo.StateChanges.Leases.FirstOrDefault();
            Assert.IsNotNull(stateChangesLease);

            // Get info

            var leasing = Node.GetLeaseInfo(leaseTx.Transaction.Id!);

            var invokeLeasing = Node.GetLeasesInfo(new List<Base58s>{ stateChangesLease.Id }).FirstOrDefault();
            Assert.IsNotNull(invokeLeasing);

            var leasingList = Node.GetLeasesInfo(leaseTx.Transaction.Id!, stateChangesLease.Id);
            var activeLeases = Node.GetActiveLeases(alice.Addr);

            // Assert active leasing

            Assert.AreEqual(leasing, new LeaseInfo {
                Id = leaseTx.Transaction.Id!,
                OriginTransactionId = leaseTx.Transaction.Id!,
                Sender = alice.Addr,
                Recipient = bob.Addr,
                Amount = leaseAmount,
                Height = leaseTx.Height,
                Status = LeaseStatus.Active,
                CancelHeight = null,
                CancelTransactionId = null
            });
            Assert.IsNull(leasing.CancelHeight);
            Assert.IsNull(leasing.CancelTransactionId);

            Assert.AreEqual(invokeLeasing, new LeaseInfo
            {
                Id = stateChangesLease.Id,
                OriginTransactionId = invokeTx.Transaction.Id!,
                Sender = bob.Addr,
                Recipient = alice.Addr,
                Amount = invokeLeaseAmount,
                Height = invokeTx.Height,
                Status = LeaseStatus.Active,
                CancelHeight = null,
                CancelTransactionId = null
            });

            Assert.AreEqual(invokeLeasing, stateChangesLease);
            Assert.AreEqual(leasingList.Count, activeLeases.Count);
            Assert.IsTrue(leasingList.Contains(leasing));
            Assert.IsTrue(leasingList.Contains(invokeLeasing));
            Assert.IsTrue(activeLeases.Contains(leasing));
            Assert.IsTrue(activeLeases.Contains(invokeLeasing));

            // 2. Cancel leasing

            var leaseCancelTx = Node.WaitForTransaction(Node.Broadcast(
                    LeaseCancelTransactionBuilder.Params(leasing.Id).GetSignedWith(alice.Pk)).Id);

            var invokeCancelTx = Node.WaitForTransaction(Node.Broadcast(
                InvokeScriptTransactionBuilder.Params(
                        bob.Addr,
                        new Call
                        {
                            Function = "cancel",
                            Args = new List<CallArg> { CallArg.AsString(invokeLeasing.Id) }
                        }).GetSignedWith(alice.Pk)).Id);

            var stateChangesCancelInfo = Node.GetTransactionInfo<InvokeScriptTransactionInfo>(invokeCancelTx.Transaction.Id!);
            Assert.IsNotNull(stateChangesCancelInfo);

            try
            {
                Node.GetTransactionInfo<TransferTransactionInfo>(invokeCancelTx.Transaction.Id!); //try to get invalid transaction info type
                Assert.Fail();
            } catch { }

            var stateChangesCancel = stateChangesCancelInfo.StateChanges.LeaseCancels.FirstOrDefault();
            Assert.IsNotNull(stateChangesCancel);

            // Get info

            var leasingCancel = Node.GetLeaseInfo(leaseTx.Transaction.Id!);
            var invokeLeasingCancel = Node.GetLeaseInfo(stateChangesCancel.Id);
            var leasingListCancel = Node.GetLeasesInfo(leaseTx.Transaction.Id!, stateChangesCancel.Id);
            var activeLeasesCancel = Node.GetActiveLeases(alice.Addr);

            // Assert canceled leasing

            Assert.AreEqual(leasingCancel, new LeaseInfo {
                Id = leaseTx.Transaction.Id!,
                OriginTransactionId = leaseTx.Transaction.Id!,
                Sender = alice.Addr,
                Recipient = bob.Addr,
                Amount = leaseAmount,
                Height = leaseTx.Height,
                Status = LeaseStatus.Canceled,
                CancelHeight = leaseCancelTx.Height,
                CancelTransactionId = leaseCancelTx.Transaction.Id
            });
            Assert.IsTrue(leasingCancel.CancelHeight > 0);
            Assert.IsNotNull(leasingCancel.CancelTransactionId);
            Assert.AreEqual(invokeLeasingCancel, new LeaseInfo
            {
                Id = stateChangesLease.Id,
                OriginTransactionId = invokeTx.Transaction.Id!,
                Sender = bob.Addr,
                Recipient = alice.Addr,
                Amount = invokeLeaseAmount,
                Height = invokeTx.Height,
                Status = LeaseStatus.Canceled,
                CancelHeight = invokeCancelTx.Height,
                CancelTransactionId = invokeCancelTx.Transaction.Id
            });

            Assert.AreEqual(0, activeLeasesCancel.Count);
        }
    }
}