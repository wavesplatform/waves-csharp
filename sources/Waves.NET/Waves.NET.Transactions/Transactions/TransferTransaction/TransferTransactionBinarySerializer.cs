using Google.Protobuf;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions
{
    public class TransferTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Signature }, { 2, TransactionSchema.Proofs }, { 3, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (ITransferTransaction)transaction;

            proto.Transfer = new TransferTransactionData();

            proto.Transfer.Recipient = tx.Recipient.Type == Address.TYPE
                ? new Recipient { PublicKeyHash = ByteString.CopyFrom(((Address)tx.Recipient).PublicKeyHash) }
                : new Recipient { PublicKeyHash = ByteString.CopyFromUtf8(((Alias)tx.Recipient).Name) };

            proto.Transfer.Amount = new AmountProto
            {
                Amount_ = tx.Amount,
                AssetId = tx.AssetId is null ? ByteString.Empty : ByteString.CopyFromUtf8(tx.AssetId)
            };

            proto.Transfer.Attachment = tx.Attachment is null ? ByteString.Empty : ByteString.CopyFrom(tx.Attachment);
        }

        protected override void SerializeToProofsSchema(BinaryWriter bw, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeToSignatureSchema(BinaryWriter bw, Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}