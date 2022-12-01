using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class ReissueTransactionBuilder : TransactionBuilder<ReissueTransactionBuilder, ReissueTransaction>
    {
        public ReissueTransactionBuilder() : base(ReissueTransaction.LatestVersion, ReissueTransaction.MinFee, ReissueTransaction.TYPE) { }

        public ReissueTransactionBuilder(AssetId assetId, long quantity, bool reissuable = true) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Quantity = quantity;
            Transaction.Reissuable = reissuable;
        }

        public static ReissueTransactionBuilder Params(AssetId assetId, long quantity, bool reissuable = true)
        {
            return new ReissueTransactionBuilder(assetId, quantity, reissuable);
        }
    }
}