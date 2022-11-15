using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class LeaseCancelTransactionBuilder : TransactionBuilder<LeaseCancelTransactionBuilder, LeaseCancelTransaction>
    {
        public LeaseCancelTransactionBuilder() : base(LeaseCancelTransaction.LatestVersion, LeaseCancelTransaction.MinFee, LeaseCancelTransaction.TYPE) { }

        public LeaseCancelTransactionBuilder(Base58s leaseId, LeaseInfo leaseInfo) : this()
        {
            Transaction.LeaseId = leaseId;
            Transaction.Lease = leaseInfo;
        }

        public static LeaseCancelTransactionBuilder Data(Base58s leaseId, LeaseInfo leaseInfo)
        {
            return new LeaseCancelTransactionBuilder(leaseId, leaseInfo);
        }
    }
}