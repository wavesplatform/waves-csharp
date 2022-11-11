using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class LeaseTransactionInfo : TransactionInfo
    {
        public LeaseTransactionInfo(LeaseTransaction transaction, string? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override LeaseTransaction Transaction => (LeaseTransaction)base.Transaction;
    }
}