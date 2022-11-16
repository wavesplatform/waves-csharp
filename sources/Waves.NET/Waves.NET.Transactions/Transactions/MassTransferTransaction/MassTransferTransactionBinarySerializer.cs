using Google.Protobuf;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class MassTransferTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 2 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IMassTransferTransaction)transaction;
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
