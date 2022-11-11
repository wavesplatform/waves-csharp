namespace Waves.NET.Transactions.Builders
{
    public class BurnTransactionBuilder : TransactionBuilder<BurnTransactionBuilder, BurnTransaction>
    {
        public BurnTransactionBuilder() : base(BurnTransaction.LatestVersion, BurnTransaction.MinFee, BurnTransaction.TYPE) { }

        public BurnTransactionBuilder(string assetId, long amount) : this()
        {
            Transaction.Amount = amount;
            Transaction.AssetId = assetId;
        }

        public BurnTransactionBuilder Data(string assetId, long amount)
        {
            return new BurnTransactionBuilder(assetId, amount);
        }
    }
}