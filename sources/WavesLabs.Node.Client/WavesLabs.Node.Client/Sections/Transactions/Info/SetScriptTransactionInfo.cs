using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class SetScriptTransactionInfo : TransactionInfo
    {
        public SetScriptTransactionInfo(SetScriptTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override SetScriptTransaction Transaction => (SetScriptTransaction)base.Transaction;
    }
}