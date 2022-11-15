using Google.Protobuf;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions
{
    public class MassTransferTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Signature }, { 2, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (IMassTransferTransaction)transaction;
            proto.MassTransfer = new MassTransferTransactionData();
            proto.MassTransfer.Transfers.Add(tx.Transfers.Select(TransferToProtobuf));
            proto.MassTransfer.AssetId = ByteString.CopyFromUtf8(tx.AssetId);
            proto.MassTransfer.Attachment = ByteString.CopyFromUtf8(tx.Attachment);
        }

        protected override void SerializeToProofsSchema(BinaryWriter bw, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeToSignatureSchema(BinaryWriter bw, Transaction transaction)
        {
            throw new NotImplementedException();
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
