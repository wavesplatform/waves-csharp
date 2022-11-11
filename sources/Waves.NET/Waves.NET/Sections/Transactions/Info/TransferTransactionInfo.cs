using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class TransferTransactionInfo : TransactionInfo
    {
        public TransferTransactionInfo(TransferTransaction transaction, string? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override TransferTransaction Transaction => (TransferTransaction)base.Transaction;
    }
}