using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class InvokeScriptTransactionBuilder : TransactionBuilder<InvokeScriptTransactionBuilder, InvokeScriptTransaction>
    {
        public InvokeScriptTransactionBuilder() : base(InvokeScriptTransaction.LatestVersion, InvokeScriptTransaction.MinFee, InvokeScriptTransaction.TYPE) { }

        public InvokeScriptTransactionBuilder(IRecipient dApp, ICollection<Amount> payment, Call call) : this()
        {
            Transaction.DApp = dApp;
            Transaction.Call = call;
            Transaction.Payment = payment ?? new List<Amount>();
        }

        public static InvokeScriptTransactionBuilder Params(IRecipient dApp, ICollection<Amount> payment, Call call)
        {
            return new InvokeScriptTransactionBuilder(dApp, payment, call);
        }

        public static InvokeScriptTransactionBuilder Params(IRecipient dApp, Call call)
        {
            return new InvokeScriptTransactionBuilder(dApp, new List<Amount>(), call);
        }
    }
}