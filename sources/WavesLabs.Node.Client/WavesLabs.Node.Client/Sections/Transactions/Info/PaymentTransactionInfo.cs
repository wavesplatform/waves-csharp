using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class PaymentTransactionInfo : TransactionInfo
    {
        public PaymentTransactionInfo(PaymentTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override PaymentTransaction Transaction => (PaymentTransaction)base.Transaction;
    }
}