using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class LeaseCancelTransactionInfo : TransactionInfo
    {
        public LeaseCancelTransactionInfo(LeaseCancelTransaction transaction, ApplicationStatus? applicationStatus, int height)
            : base(transaction, applicationStatus, height) { }

        public override LeaseCancelTransaction Transaction => (LeaseCancelTransaction)base.Transaction;

        public LeaseInfo Lease => Transaction.Lease;
    }
}