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
    }
}