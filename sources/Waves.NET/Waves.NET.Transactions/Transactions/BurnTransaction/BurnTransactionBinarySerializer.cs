using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class BurnTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> {{ 1, TransactionSchema.Signature },{ 2, TransactionSchema.Proofs },{ 3, TransactionSchema.Protobuf }};

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (IBurnTransaction)transaction;
            proto.Burn = new BurnTransactionData();
            proto.Burn.AssetAmount = new AmountProto
            {
                Amount_ = tx.Amount,
                AssetId = tx.AssetId is null ? ByteString.Empty : ByteString.CopyFrom(tx.AssetId)
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
