using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class ExchangeTransactionInfo : TransactionInfo
    {
        public ExchangeTransactionInfo(ExchangeTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override ExchangeTransaction Transaction => (ExchangeTransaction)base.Transaction;
    }
}