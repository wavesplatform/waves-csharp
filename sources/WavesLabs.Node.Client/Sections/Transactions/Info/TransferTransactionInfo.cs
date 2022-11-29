using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class TransferTransactionInfo : TransactionInfo
    {
        public TransferTransactionInfo(TransferTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override TransferTransaction Transaction => (TransferTransaction)base.Transaction;
    }
}