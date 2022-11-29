using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class ReissueTransactionInfo : TransactionInfo
    {
        public ReissueTransactionInfo(ReissueTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override ReissueTransaction Transaction => (ReissueTransaction)base.Transaction;
    }
}