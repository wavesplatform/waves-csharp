using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class InvokeScriptTransactionInfo : TransactionInfo
    {
        public InvokeScriptTransactionInfo(InvokeScriptTransaction transaction, string? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override InvokeScriptTransaction Transaction => (InvokeScriptTransaction)base.Transaction;
    }
}