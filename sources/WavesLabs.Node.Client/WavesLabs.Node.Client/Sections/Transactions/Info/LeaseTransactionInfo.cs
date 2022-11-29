using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class LeaseTransactionInfo : TransactionInfo
    {
        public LeaseTransactionInfo(LeaseTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) { }

        public override LeaseTransaction Transaction => (LeaseTransaction)base.Transaction;

        public LeaseStatus Status => Transaction.Status;
    }
}