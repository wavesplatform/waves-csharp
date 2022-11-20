using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class LeaseCancelTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 3 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (ILeaseCancelTransaction)transaction;
            proto.LeaseCancel = new LeaseCancelTransactionData();
            proto.LeaseCancel.LeaseId = ByteString.CopyFrom(tx.LeaseId);
        }
    }
}