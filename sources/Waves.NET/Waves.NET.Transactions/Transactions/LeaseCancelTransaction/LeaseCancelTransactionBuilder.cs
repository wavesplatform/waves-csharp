using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class LeaseCancelTransactionBuilder : TransactionBuilder<LeaseCancelTransactionBuilder, LeaseCancelTransaction>
    {
        public LeaseCancelTransactionBuilder() : base(LeaseCancelTransaction.LatestVersion, LeaseCancelTransaction.MinFee, LeaseCancelTransaction.TYPE) { }

        public LeaseCancelTransactionBuilder(Base58s leaseId) : this()
        {
            Transaction.LeaseId = leaseId;
        }

        public static LeaseCancelTransactionBuilder Params(Base58s leaseId)
        {
            return new LeaseCancelTransactionBuilder(leaseId);
        }
    }
}