using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class SetAssetScriptTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Signature }, { 2, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (ISetAssetScriptTransaction)transaction;
            proto.SetAssetScript = new SetAssetScriptTransactionData();
            proto.SetAssetScript.AssetId = ByteString.CopyFromUtf8(tx.AssetId);
            proto.SetAssetScript.Script = ByteString.CopyFromUtf8(tx.Script);
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