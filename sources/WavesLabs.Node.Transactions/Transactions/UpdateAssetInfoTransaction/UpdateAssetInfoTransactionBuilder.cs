using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class UpdateAssetInfoTransactionBuilder : TransactionBuilder<UpdateAssetInfoTransactionBuilder, UpdateAssetInfoTransaction>
    {
        public UpdateAssetInfoTransactionBuilder() : base(UpdateAssetInfoTransaction.LatestVersion, UpdateAssetInfoTransaction.MinFee, UpdateAssetInfoTransaction.TYPE) { }

        public UpdateAssetInfoTransactionBuilder(AssetId assetId, string name, string description) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Name = name;
            Transaction.Description = description;
        }

        public static UpdateAssetInfoTransactionBuilder Params(AssetId assetId, string name, string description)
        {
            return new UpdateAssetInfoTransactionBuilder(assetId, name, description);
        }
    }
}