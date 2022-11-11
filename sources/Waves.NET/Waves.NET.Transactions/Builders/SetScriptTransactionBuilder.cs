namespace Waves.NET.Transactions.Builders
{
    public class SetScriptTransactionBuilder : TransactionBuilder<SetScriptTransactionBuilder, SetScriptTransaction>
    {
        public SetScriptTransactionBuilder() : base(SetScriptTransaction.LatestVersion, SetScriptTransaction.MinFee, SetScriptTransaction.TYPE) { }

        public SetScriptTransactionBuilder(string script) : this()
        {
            Transaction.Script = script;
        }

        public static SetScriptTransactionBuilder Data(string script)
        {
            return new SetScriptTransactionBuilder(script);
        }
    }
}