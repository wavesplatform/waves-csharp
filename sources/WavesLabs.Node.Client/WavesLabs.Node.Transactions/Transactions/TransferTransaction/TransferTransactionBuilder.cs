using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class TransferTransactionBuilder : TransactionBuilder<TransferTransactionBuilder, TransferTransaction>
    {
        public TransferTransactionBuilder() : base(TransferTransaction.LatestVersion, TransferTransaction.MinFee, TransferTransaction.TYPE) { }

        public TransferTransactionBuilder(IRecipient recipient, long amount, Base58s? assetId = null, Base58s? feeAsset = null, string? attachment = null) : this()
        {
            Transaction.Recipient = recipient;
            Transaction.Amount = amount;
            Transaction.Attachment = attachment;
            Transaction.AssetId = assetId;
            Transaction.FeeAsset = feeAsset;
        }

        public static TransferTransactionBuilder Params(IRecipient recipient, long amount, Base58s? assetId = null, Base58s? feeAsset = null, string? attachment = null)
        {
            return new TransferTransactionBuilder(recipient, amount, assetId, feeAsset, attachment);
        }
    }
}