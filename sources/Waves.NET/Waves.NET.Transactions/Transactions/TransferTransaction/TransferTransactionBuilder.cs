using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class TransferTransactionBuilder : TransactionBuilder<TransferTransactionBuilder, TransferTransaction>
    {
        public TransferTransactionBuilder() : base(TransferTransaction.LatestVersion, TransferTransaction.MinFee, TransferTransaction.TYPE) { }

        public TransferTransactionBuilder(IRecipient recipient, long amount, Base58s? assetId = null, Base58s? feeAsset = null, Base58s? attachment = null) : this()
        {
            Transaction.Recipient = recipient;
            Transaction.Amount = amount;
            Transaction.Attachment = attachment;
            Transaction.AssetId = assetId;
            Transaction.FeeAsset = feeAsset;
        }

        public static TransferTransactionBuilder Data(IRecipient recipient, long amount, Base58s? assetId = null, Base58s? feeAsset = null, Base58s? attachment = null)
        {
            return new TransferTransactionBuilder(recipient, amount, assetId, feeAsset, attachment);
        }
    }
}