using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class IssueTransactionInfo : TransactionInfo
    {
        public IssueTransactionInfo(IssueTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override IssueTransaction Transaction => (IssueTransaction)base.Transaction;
    }
}