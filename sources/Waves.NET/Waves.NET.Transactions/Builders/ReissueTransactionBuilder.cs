namespace Waves.NET.Transactions.Builders
{
    public class ReissueTransactionBuilder : TransactionBuilder<ReissueTransactionBuilder, ReissueTransaction>
    {
        public ReissueTransactionBuilder() : base(ReissueTransaction.LatestVersion, ReissueTransaction.MinFee, ReissueTransaction.TYPE) { }

        public ReissueTransactionBuilder(string assetId, long quantity, bool reissuable) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Quantity = quantity;
            Transaction.Reissuable = reissuable;
        }

        public static ReissueTransactionBuilder Data(string assetId, long quantity, bool reissuable)
        {
            return new ReissueTransactionBuilder(assetId, quantity, reissuable);
        }
    }
}