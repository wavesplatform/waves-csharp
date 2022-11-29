using Google.Protobuf;
using Waves;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class LeaseTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 3 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (ILeaseTransaction)transaction;
            proto.Lease = new LeaseTransactionData();
            proto.Lease.Amount = tx.Amount;
            proto.Lease.Recipient = proto.Lease.Recipient = tx.Recipient.Type == Address.TYPE
                ? new Recipient { PublicKeyHash = ByteString.CopyFrom(((Address)tx.Recipient).PublicKeyHash) }
                : new Recipient { PublicKeyHash = ByteString.CopyFrom(((Alias)tx.Recipient).Bytes) };
        }
    }
}