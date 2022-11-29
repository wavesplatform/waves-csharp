using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class SetAssetScriptTransactionInfo : TransactionInfo
    {
        public SetAssetScriptTransactionInfo(SetAssetScriptTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override SetAssetScriptTransaction Transaction => (SetAssetScriptTransaction)base.Transaction;
    }
}