using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class DataTransactionInfo : TransactionInfo
    {
        public DataTransactionInfo(DataTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) { }

        public override DataTransaction Transaction => (DataTransaction)base.Transaction;
    }
}