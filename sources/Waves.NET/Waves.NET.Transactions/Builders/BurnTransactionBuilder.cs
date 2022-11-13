using Google.Protobuf;

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

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (IBurnTransaction)Transaction;
            proto.Burn = new BurnTransactionData();
            proto.Burn.AssetAmount = new AmountProto
            {
                Amount_ = tx.Amount,
                AssetId = string.IsNullOrWhiteSpace(tx.AssetId) ? ByteString.Empty : ByteString.CopyFromUtf8(tx.AssetId)
            };
        }
    }
}