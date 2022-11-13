using Google.Protobuf;

namespace Waves.NET.Transactions.Builders
{
    public class SponsorFeeTransactionBuilder : TransactionBuilder<SponsorFeeTransactionBuilder, SponsorFeeTransaction>
    {
        public SponsorFeeTransactionBuilder() : base(SponsorFeeTransaction.LatestVersion, SponsorFeeTransaction.MinFee, SponsorFeeTransaction.TYPE) { }

        public SponsorFeeTransactionBuilder(string assetId, long minSponsoredAssetFee) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.MinSponsoredAssetFee = minSponsoredAssetFee;
        }

        public static SponsorFeeTransactionBuilder Data(string assetId, long minSponsoredAssetFee)
        {
            return new SponsorFeeTransactionBuilder(assetId, minSponsoredAssetFee);
        }

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (ISponsorFeeTransaction)Transaction;
            proto.SponsorFee = new SponsorFeeTransactionData();
            proto.SponsorFee.MinFee = new AmountProto
            {
                Amount_ = tx.MinSponsoredAssetFee,
                AssetId = ByteString.CopyFromUtf8(tx.AssetId)
            };
        }
    }
}