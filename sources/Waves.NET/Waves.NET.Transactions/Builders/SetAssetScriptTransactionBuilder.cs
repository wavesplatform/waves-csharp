using Google.Protobuf;

namespace Waves.NET.Transactions.Builders
{
    public class SetAssetScriptTransactionBuilder : TransactionBuilder<SetAssetScriptTransactionBuilder, SetAssetScriptTransaction>
    {
        public SetAssetScriptTransactionBuilder() : base(SetAssetScriptTransaction.LatestVersion, SetAssetScriptTransaction.MinFee, SetAssetScriptTransaction.TYPE) { }

        public SetAssetScriptTransactionBuilder(string assetId, string script) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Script = script;
        }

        public static SetAssetScriptTransactionBuilder Data(string assetId, string script)
        {
            return new SetAssetScriptTransactionBuilder(assetId, script);
        }

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (ISetAssetScriptTransaction)Transaction;
            proto.SetAssetScript = new SetAssetScriptTransactionData();
            proto.SetAssetScript.AssetId = ByteString.CopyFromUtf8(tx.AssetId);
            proto.SetAssetScript.Script = ByteString.CopyFromUtf8(tx.Script);
        }
    }
}