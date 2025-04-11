using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class SetScriptTransactionBuilder : TransactionBuilder<SetScriptTransactionBuilder, SetScriptTransaction>
    {
        public SetScriptTransactionBuilder() : base(SetScriptTransaction.LatestVersion, SetScriptTransaction.MinFee, SetScriptTransaction.TYPE) { }

        public SetScriptTransactionBuilder(Base64s script) : this()
        {
            Transaction.Script = script;
        }

        public static SetScriptTransactionBuilder Params(Base64s script)
        {
            return new SetScriptTransactionBuilder(script);
        }
    }
}