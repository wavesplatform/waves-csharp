using Google.Protobuf;

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

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (ISetScriptTransaction)Transaction;
            proto.SetScript = new SetScriptTransactionData();
            proto.SetScript.Script = ByteString.CopyFromUtf8(tx.Script);
        }
    }
}