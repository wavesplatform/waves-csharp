using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class ReissueTransactionBuilder : TransactionBuilder<ReissueTransactionBuilder, ReissueTransaction>
    {
        public ReissueTransactionBuilder() : base(ReissueTransaction.LatestVersion, ReissueTransaction.MinFee, ReissueTransaction.TYPE) { }

        public ReissueTransactionBuilder(Base58s assetId, long quantity, bool reissuable) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Quantity = quantity;
            Transaction.Reissuable = reissuable;
        }

        public static ReissueTransactionBuilder Params(Base58s assetId, long quantity, bool reissuable)
        {
            return new ReissueTransactionBuilder(assetId, quantity, reissuable);
        }
    }
}