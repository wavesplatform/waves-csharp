using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class InvokeScriptTransactionBuilder : TransactionBuilder<InvokeScriptTransactionBuilder, InvokeScriptTransaction>
    {
        public InvokeScriptTransactionBuilder() : base(InvokeScriptTransaction.LatestVersion, InvokeScriptTransaction.MinFee, InvokeScriptTransaction.TYPE) { }

        public InvokeScriptTransactionBuilder(IRecipient dApp, ICollection<Payment> payment, Call call, StateChanges stateChanges) : this()
        {
            Transaction.DApp = dApp;
            Transaction.Call = call;
            Transaction.Payment = payment ?? new List<Payment>();
            Transaction.StateChanges = stateChanges;
        }

        public static InvokeScriptTransactionBuilder Params(IRecipient dApp, ICollection<Payment> payment, Call call, StateChanges stateChanges)
        {
            return new InvokeScriptTransactionBuilder(dApp, payment, call, stateChanges);
        }
    }
}