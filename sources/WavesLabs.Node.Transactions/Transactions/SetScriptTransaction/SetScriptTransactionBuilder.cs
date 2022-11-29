namespace WavesLabs.Node.Transactions
{
    public class SetScriptTransactionBuilder : TransactionBuilder<SetScriptTransactionBuilder, SetScriptTransaction>
    {
        public SetScriptTransactionBuilder() : base(SetScriptTransaction.LatestVersion, SetScriptTransaction.MinFee, SetScriptTransaction.TYPE) { }

        public SetScriptTransactionBuilder(string script) : this()
        {
            Transaction.Script = script;
        }

        public static SetScriptTransactionBuilder Params(string script)
        {
            return new SetScriptTransactionBuilder(script);
        }
    }
}