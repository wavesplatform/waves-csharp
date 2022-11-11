using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class SetScriptTransactionInfo : TransactionInfo
    {
        public SetScriptTransactionInfo(SetScriptTransaction transaction, string? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override SetScriptTransaction Transaction => (SetScriptTransaction)base.Transaction;
    }
}