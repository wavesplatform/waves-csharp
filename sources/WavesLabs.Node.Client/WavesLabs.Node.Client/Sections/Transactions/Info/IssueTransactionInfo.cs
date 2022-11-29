using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class IssueTransactionInfo : TransactionInfo
    {
        public IssueTransactionInfo(IssueTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override IssueTransaction Transaction => (IssueTransaction)base.Transaction;
    }
}