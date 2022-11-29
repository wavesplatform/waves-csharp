using Google.Protobuf;
using Waves;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class TransferTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 3 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
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

            proto.Transfer.Attachment = tx.Attachment is null ? ByteString.Empty : ByteString.CopyFromUtf8(tx.Attachment);
        }
    }
}