using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class LeaseCancelTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Signature }, { 2, TransactionSchema.Proofs }, { 3, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (ILeaseCancelTransaction)transaction;
            proto.LeaseCancel = new LeaseCancelTransactionData();
            proto.LeaseCancel.LeaseId = ByteString.CopyFromUtf8(tx.LeaseId);
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