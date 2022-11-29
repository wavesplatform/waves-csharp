using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class MassTransferTransactionInfo : TransactionInfo
    {
        public MassTransferTransactionInfo(MassTransferTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override MassTransferTransaction Transaction => (MassTransferTransaction)base.Transaction;
    }
}