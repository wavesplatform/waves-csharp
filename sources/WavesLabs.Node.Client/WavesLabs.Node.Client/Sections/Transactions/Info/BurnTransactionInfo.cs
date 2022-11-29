using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class BurnTransactionInfo : TransactionInfo
    {
        public BurnTransactionInfo(BurnTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override BurnTransaction Transaction => (BurnTransaction)base.Transaction;
    }
}