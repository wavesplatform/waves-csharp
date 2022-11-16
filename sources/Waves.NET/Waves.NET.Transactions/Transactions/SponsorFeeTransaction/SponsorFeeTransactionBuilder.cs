using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class SponsorFeeTransactionBuilder : TransactionBuilder<SponsorFeeTransactionBuilder, SponsorFeeTransaction>
    {
        public SponsorFeeTransactionBuilder() : base(SponsorFeeTransaction.LatestVersion, SponsorFeeTransaction.MinFee, SponsorFeeTransaction.TYPE) { }

        public SponsorFeeTransactionBuilder(Base58s assetId, long minSponsoredAssetFee) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.MinSponsoredAssetFee = minSponsoredAssetFee;
        }

        public static SponsorFeeTransactionBuilder Params(Base58s assetId, long minSponsoredAssetFee)
        {
            return new SponsorFeeTransactionBuilder(assetId, minSponsoredAssetFee);
        }
    }
}