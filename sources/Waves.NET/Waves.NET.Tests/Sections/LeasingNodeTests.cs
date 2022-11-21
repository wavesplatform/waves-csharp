using Waves.NET.Transactions;

namespace Waves.NET.Tests.Sections
{
    [TestClass]
    public class LeasingNodeTests : NodeTestBase
    {
        [TestMethod]
        public void LeaseInfoTest()
        {
            var alice = CreateAccountWithBalance(10000000);
            var bob = CreateAccountWithBalance(10000000);

            var dAppScript = Node.Utils.CompileScript(
                "{-# STDLIB_VERSION 5 #-}\n{-# CONTENT_TYPE DAPP #-}\n{-# SCRIPT_TYPE ACCOUNT #-}\n" +
                        "@Callable(inv)\nfunc lease(amount: Int) = [Lease(inv.caller, amount)]\n" +
                        "@Callable(inv)\nfunc cancel(leaseId: String) = [LeaseCancel(leaseId.fromBase58String())]").Script;

            WaitForTransaction(Node.Transactions.Broadcast(SetScriptTransactionBuilder.Params(dAppScript!).GetSignedWith(bob.Pk)).Id);

            // 1. Send leasing

            var leaseAmount = 10000L;
            var invokeLeaseAmount = 20000L;

            var leaseTx = WaitForTransaction(Node.Transactions.Broadcast(
                LeaseTransactionBuilder.Params(bob.Addr, leaseAmount).GetSignedWith(alice.Pk)).Id);

            var invokeTx = WaitForTransaction(Node.Transactions.Broadcast(
                    InvokeScriptTransactionBuilder.Params(
                        bob.Addr,
                        new Call {
                            Function = "lease",
                            Args = new List<CallArgs> { new CallArgs { Type = CallArgType.Integer, Value = invokeLeaseAmount } }
                        }
                    ).GetSignedWith(alice.Pk)).Id);

            var txInfo = Node.Transactions.GetTransactionInfo(invokeTx.Transaction.Id!) as InvokeScriptTransactionInfo;
            Assert.IsNotNull(txInfo);

            var stateChangesLease = txInfo.StateChanges.Leases.FirstOrDefault();
            Assert.IsNotNull(stateChangesLease);

            // Get info

            var leasing = Node.Leasing.GetLeaseInfo(leaseTx.Transaction.Id!);
            var invokeLeasing = Node.Leasing.GetLeaseInfo(stateChangesLease.Id);
            var leasingList = Node.Leasing.GetLeasesInfo(leaseTx.Transaction.Id!, stateChangesLease.Id);
            var activeLeases = Node.Leasing.GetActiveLeases(alice.Addr);

            // Assert active leasing

            Assert.AreEqual(leasing, new LeaseInfo {
                Id = leaseTx.Transaction.Id!,
                OriginTransactionId = leaseTx.Transaction.Id!,
                Sender = alice.Addr,
                Recipient = bob.Addr,
                Amount = leaseAmount,
                Height = leaseTx.Height,
                Status = LeaseStatus.Active,
                CancelHeight = 0,
                CancelTransactionId = null
            });
            Assert.IsTrue(leasing.CancelHeight == 0);
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
                CancelHeight = 0,
                CancelTransactionId = null
            });

            Assert.AreEqual(invokeLeasing, stateChangesLease);
            Assert.IsTrue(leasingList.Contains(leasing));
            Assert.IsTrue(leasingList.Contains(invokeLeasing));
            Assert.IsTrue(activeLeases.SequenceEqual(leasingList));

            // 2. Cancel leasing

            var leaseCancelTx = WaitForTransaction(Node.Transactions.Broadcast(
                    LeaseCancelTransactionBuilder.Params(leasing.Id).GetSignedWith(alice.Pk)).Id);

            var invokeCancelTx = WaitForTransaction(Node.Transactions.Broadcast(
                InvokeScriptTransactionBuilder.Params(
                        bob.Addr,
                        new Call
                        {
                            Function = "cancel",
                            Args = new List<CallArgs> { new CallArgs { Type = CallArgType.String, Value = invokeLeasing.Id } }
                        }).GetSignedWith(alice.Pk)).Id);

            var stateChangesCancelInfo = Node.Transactions.GetTransactionInfo<InvokeScriptTransactionInfo>(invokeCancelTx.Transaction.Id!);
            Assert.IsNotNull(stateChangesCancelInfo);

            try
            {
                Node.Transactions.GetTransactionInfo<TransferTransactionInfo>(invokeCancelTx.Transaction.Id!); //try to get invalid transaction info type
                Assert.Fail();
            } catch { }

            var stateChangesCancel = stateChangesCancelInfo.StateChanges.LeaseCancels.FirstOrDefault();
            Assert.IsNotNull(stateChangesCancel);

            // Get info

            var leasingCancel = Node.Leasing.GetLeaseInfo(leaseTx.Transaction.Id!);
            var invokeLeasingCancel = Node.Leasing.GetLeaseInfo(stateChangesCancel.LeaseId);
            var leasingListCancel = Node.Leasing.GetLeasesInfo(leaseTx.Transaction.Id!, stateChangesCancel.LeaseId);
            var activeLeasesCancel = Node.Leasing.GetActiveLeases(alice.Addr);

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

            Assert.AreEqual(invokeLeasingCancel, stateChangesCancel);
            Assert.IsTrue(leasingList.Contains(leasingCancel));
            Assert.IsTrue(leasingList.Contains(invokeLeasingCancel));

            Assert.AreEqual(0, activeLeasesCancel.Count);
        }
    }
}