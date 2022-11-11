using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class BurnTransactionInfo : TransactionInfo
    {
        public BurnTransactionInfo(BurnTransaction transaction, string? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override BurnTransaction Transaction => (BurnTransaction)base.Transaction;
    }
}