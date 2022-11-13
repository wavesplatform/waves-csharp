using Google.Protobuf;

namespace Waves.NET.Transactions.Builders
{
    public class LeaseCancelTransactionBuilder : TransactionBuilder<LeaseCancelTransactionBuilder, LeaseCancelTransaction>
    {
        public LeaseCancelTransactionBuilder() : base(LeaseCancelTransaction.LatestVersion, LeaseCancelTransaction.MinFee, LeaseCancelTransaction.TYPE) { }

        public LeaseCancelTransactionBuilder(string leaseId, LeaseInfo leaseInfo) : this()
        {
            Transaction.LeaseId = leaseId;
            Transaction.Lease = leaseInfo;
        }

        public static LeaseCancelTransactionBuilder Data(string leaseId, LeaseInfo leaseInfo)
        {
            return new LeaseCancelTransactionBuilder(leaseId, leaseInfo);
        }

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (ILeaseCancelTransaction)Transaction;
            proto.LeaseCancel = new LeaseCancelTransactionData();
            proto.LeaseCancel.LeaseId = ByteString.CopyFromUtf8(tx.LeaseId);
        }
    }
}