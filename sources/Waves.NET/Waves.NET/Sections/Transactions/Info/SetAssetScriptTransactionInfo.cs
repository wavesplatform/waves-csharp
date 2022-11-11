using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class SetAssetScriptTransactionInfo : TransactionInfo
    {
        public SetAssetScriptTransactionInfo(SetAssetScriptTransaction transaction, string? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override SetAssetScriptTransaction Transaction => (SetAssetScriptTransaction)base.Transaction;
    }
}