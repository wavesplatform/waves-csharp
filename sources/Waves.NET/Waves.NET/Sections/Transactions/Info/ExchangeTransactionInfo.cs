using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class ExchangeTransactionInfo : TransactionInfo
    {
        public ExchangeTransactionInfo(ExchangeTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override ExchangeTransaction Transaction => (ExchangeTransaction)base.Transaction;
    }
}