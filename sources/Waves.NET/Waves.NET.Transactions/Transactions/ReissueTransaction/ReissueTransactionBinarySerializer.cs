using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class ReissueTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Signature }, { 2, TransactionSchema.Proofs }, { 3, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (IReissueTransaction)transaction;
            proto.Reissue = new ReissueTransactionData();
            proto.Reissue.Reissuable = tx.Reissuable;
            proto.Reissue.AssetAmount = new AmountProto
            {
                Amount_ = tx.Amount,
                AssetId = ByteString.CopyFromUtf8(tx.AssetId)
            };
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