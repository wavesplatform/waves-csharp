using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class LeaseTransactionInfo : TransactionInfo
    {
        public LeaseTransactionInfo(LeaseTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) { }

        public override LeaseTransaction Transaction => (LeaseTransaction)base.Transaction;

        public LeaseStatus Status => Transaction.Status;
    }
}