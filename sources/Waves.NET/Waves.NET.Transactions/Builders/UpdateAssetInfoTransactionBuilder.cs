namespace Waves.NET.Transactions.Builders
{
    public class UpdateAssetInfoTransactionBuilder : TransactionBuilder<UpdateAssetInfoTransactionBuilder, UpdateAssetInfoTransaction>
    {
        public UpdateAssetInfoTransactionBuilder() : base(UpdateAssetInfoTransaction.LatestVersion, UpdateAssetInfoTransaction.MinFee, UpdateAssetInfoTransaction.TYPE) { }

        public UpdateAssetInfoTransactionBuilder(string assetId, string name, string description) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Name = name;
            Transaction.Description = description;
        }

        public static UpdateAssetInfoTransactionBuilder Data(string assetId, string name, string description)
        {
            return new UpdateAssetInfoTransactionBuilder(assetId, name, description);
        }
    }
}