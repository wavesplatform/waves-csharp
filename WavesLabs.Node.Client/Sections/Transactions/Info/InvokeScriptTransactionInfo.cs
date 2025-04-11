using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class InvokeScriptTransactionInfo : TransactionInfo
    {
        public InvokeScriptTransactionInfo(InvokeScriptTransaction transaction, ApplicationStatus? applicationStatus, int height)
            : base(transaction, applicationStatus, height) { }

        public override InvokeScriptTransaction Transaction => (InvokeScriptTransaction)base.Transaction;

        public StateChanges StateChanges => Transaction.StateChanges;
    }
}