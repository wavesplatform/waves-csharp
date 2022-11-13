using Google.Protobuf;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions.Builders
{
    public class TransferTransactionBuilder : TransactionBuilder<TransferTransactionBuilder, TransferTransaction>
    {
        public TransferTransactionBuilder() : base(TransferTransaction.LatestVersion, TransferTransaction.MinFee, TransferTransaction.TYPE) { }

        public TransferTransactionBuilder(IRecipient recipient, long amount, string? assetId, string? feeAsset, string attachment = "") : this()
        {
            Transaction.Recipient = recipient;
            Transaction.Amount = amount;
            Transaction.Attachment = attachment;
            Transaction.AssetId = assetId;
            Transaction.FeeAsset = feeAsset;
        }

        public static TransferTransactionBuilder Data(IRecipient recipient, long amount, string? assetId = null, string? feeAsset = null, string attachment = "")
        {
            return new TransferTransactionBuilder(recipient, amount, assetId, feeAsset, attachment);
        }

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (ITransferTransaction)Transaction;

            proto.Transfer = new TransferTransactionData();

            proto.Transfer.Recipient = tx.Recipient.Type == Address.TYPE
                ? new Recipient { PublicKeyHash = ByteString.CopyFrom(((Address)tx.Recipient).PublicKeyHash) }
                : new Recipient { PublicKeyHash = ByteString.CopyFromUtf8(((Alias)tx.Recipient).Name) };

            proto.Transfer.Amount = new AmountProto
            {
                Amount_ = tx.Amount,
                AssetId = string.IsNullOrWhiteSpace(tx.AssetId) ? ByteString.Empty : ByteString.CopyFromUtf8(tx.AssetId)
            };

            proto.Transfer.Attachment = ByteString.CopyFromUtf8(tx.Attachment);
        }
    }
}