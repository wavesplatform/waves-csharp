using Google.Protobuf;

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

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (IReissueTransaction)Transaction;
            proto.Reissue = new ReissueTransactionData();
            proto.Reissue.Reissuable = tx.Reissuable;
            proto.Reissue.AssetAmount = new AmountProto
            {
                Amount_ = tx.Amount,
                AssetId = ByteString.CopyFromUtf8(tx.AssetId)
            };
        }
    }
}