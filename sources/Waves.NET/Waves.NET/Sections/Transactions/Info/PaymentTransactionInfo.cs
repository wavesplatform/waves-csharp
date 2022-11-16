using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class PaymentTransactionInfo : TransactionInfo
    {
        public PaymentTransactionInfo(PaymentTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override PaymentTransaction Transaction => (PaymentTransaction)base.Transaction;
    }
}