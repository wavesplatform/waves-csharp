using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class SetAssetScriptTransactionBuilder : TransactionBuilder<SetAssetScriptTransactionBuilder, SetAssetScriptTransaction>
    {
        public SetAssetScriptTransactionBuilder() : base(SetAssetScriptTransaction.LatestVersion, SetAssetScriptTransaction.MinFee, SetAssetScriptTransaction.TYPE) { }

        public SetAssetScriptTransactionBuilder(AssetId assetId, string script) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Script = script;
        }

        public static SetAssetScriptTransactionBuilder Params(AssetId assetId, string script)
        {
            return new SetAssetScriptTransactionBuilder(assetId, script);
        }
    }
}