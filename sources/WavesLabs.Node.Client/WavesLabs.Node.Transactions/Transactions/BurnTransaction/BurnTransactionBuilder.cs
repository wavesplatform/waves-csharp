using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class BurnTransactionBuilder : TransactionBuilder<BurnTransactionBuilder, BurnTransaction>
    {
        public BurnTransactionBuilder() : base(BurnTransaction.LatestVersion, BurnTransaction.MinFee, BurnTransaction.TYPE) { }

        public BurnTransactionBuilder(Base58s assetId, long amount) : this()
        {
            Transaction.Amount = amount;
            Transaction.AssetId = assetId;
        }

        public static BurnTransactionBuilder Params(Base58s assetId, long amount)
        {
            return new BurnTransactionBuilder(assetId, amount);
        }
    }
}