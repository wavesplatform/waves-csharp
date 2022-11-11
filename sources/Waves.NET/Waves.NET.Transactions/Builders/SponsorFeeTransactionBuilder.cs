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
    }
}