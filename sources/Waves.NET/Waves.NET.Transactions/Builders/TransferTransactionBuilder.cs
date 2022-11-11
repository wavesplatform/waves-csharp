namespace Waves.NET.Transactions.Builders
{
    public class TransferTransactionBuilder : TransactionBuilder<TransferTransactionBuilder, TransferTransaction>
    {
        public TransferTransactionBuilder() : base(TransferTransaction.LatestVersion, TransferTransaction.MinFee, TransferTransaction.TYPE) { }

        public TransferTransactionBuilder(string recipient, long amount, string assetId, string feeAsset, string attachment = "") : this()
        {
            Transaction.Recipient = recipient;
            Transaction.Amount = amount;
            Transaction.Attachment = attachment;
            Transaction.AssetId = assetId;
            Transaction.FeeAsset = feeAsset;
        }

        public static TransferTransactionBuilder Data(string recipient, long amount, string assetId = "", string feeAsset = "", string attachment = "")
        {
            return new TransferTransactionBuilder(recipient, amount, assetId, feeAsset, attachment);
        }
    }
}