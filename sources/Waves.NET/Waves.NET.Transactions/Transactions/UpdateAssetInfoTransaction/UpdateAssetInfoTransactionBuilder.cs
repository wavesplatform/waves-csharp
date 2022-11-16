using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class UpdateAssetInfoTransactionBuilder : TransactionBuilder<UpdateAssetInfoTransactionBuilder, UpdateAssetInfoTransaction>
    {
        public UpdateAssetInfoTransactionBuilder() : base(UpdateAssetInfoTransaction.LatestVersion, UpdateAssetInfoTransaction.MinFee, UpdateAssetInfoTransaction.TYPE) { }

        public UpdateAssetInfoTransactionBuilder(Base58s assetId, string name, string description) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Name = name;
            Transaction.Description = description;
        }

        public static UpdateAssetInfoTransactionBuilder Params(Base58s assetId, string name, string description)
        {
            return new UpdateAssetInfoTransactionBuilder(assetId, name, description);
        }
    }
}