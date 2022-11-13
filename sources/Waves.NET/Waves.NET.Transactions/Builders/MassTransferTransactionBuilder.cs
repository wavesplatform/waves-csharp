using Google.Protobuf;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions.Builders
{
    public class MassTransferTransactionBuilder : TransactionBuilder<MassTransferTransactionBuilder, MassTransferTransaction>
    {
        public MassTransferTransactionBuilder() : base(MassTransferTransaction.LatestVersion, MassTransferTransaction.MinFee, MassTransferTransaction.TYPE) { }

        public MassTransferTransactionBuilder(string assetId, string attachment, int transferCount, long totalAmount, ICollection<Transfer> transfer) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Attachment = attachment;
            Transaction.TransferCount = transferCount;
            Transaction.TotalAmount = totalAmount;
            Transaction.Transfers = transfer;
        }

        public static MassTransferTransactionBuilder Data(string assetId, string attachment, int transferCount, long totalAmount, ICollection<Transfer> transfer)
        {
            return new MassTransferTransactionBuilder(assetId, attachment, transferCount, totalAmount, transfer);
        }

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (IMassTransferTransaction)Transaction;
            proto.MassTransfer = new MassTransferTransactionData();
            proto.MassTransfer.Transfers.Add(tx.Transfers.Select(TransferToProtobuf));
            proto.MassTransfer.AssetId = ByteString.CopyFromUtf8(tx.AssetId);
            proto.MassTransfer.Attachment = ByteString.CopyFromUtf8(tx.Attachment);
        }

        private MassTransferTransactionData.Types.Transfer TransferToProtobuf(Transfer transfer)
        {
            return new MassTransferTransactionData.Types.Transfer
            {
                Amount = transfer.Amount,
                Recipient = transfer.Recipient.Type == Address.TYPE
                ? new Recipient { PublicKeyHash = ByteString.CopyFrom(((Address)transfer.Recipient).PublicKeyHash) }
                : new Recipient { PublicKeyHash = ByteString.CopyFromUtf8(((Alias)transfer.Recipient).Name) }
            };
        }
    }
}