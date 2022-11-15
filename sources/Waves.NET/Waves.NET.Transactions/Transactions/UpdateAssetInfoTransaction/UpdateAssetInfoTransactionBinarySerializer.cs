using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class UpdateAssetInfoTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (IUpdateAssetInfoTransaction)transaction;
            proto.UpdateAssetInfo = new UpdateAssetInfoTransactionData();
            proto.UpdateAssetInfo.AssetId = ByteString.CopyFromUtf8(tx.AssetId);
            proto.UpdateAssetInfo.Name = tx.Name;
            proto.UpdateAssetInfo.Description = tx.Description;
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
