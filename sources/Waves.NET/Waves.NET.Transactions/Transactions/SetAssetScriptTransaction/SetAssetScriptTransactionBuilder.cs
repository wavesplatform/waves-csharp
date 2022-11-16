using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class SetAssetScriptTransactionBuilder : TransactionBuilder<SetAssetScriptTransactionBuilder, SetAssetScriptTransaction>
    {
        public SetAssetScriptTransactionBuilder() : base(SetAssetScriptTransaction.LatestVersion, SetAssetScriptTransaction.MinFee, SetAssetScriptTransaction.TYPE) { }

        public SetAssetScriptTransactionBuilder(Base58s assetId, string script) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Script = script;
        }

        public static SetAssetScriptTransactionBuilder Params(Base58s assetId, string script)
        {
            return new SetAssetScriptTransactionBuilder(assetId, script);
        }
    }
}