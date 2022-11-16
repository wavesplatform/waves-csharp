using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class SetAssetScriptTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 2 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (ISetAssetScriptTransaction)transaction;
            proto.SetAssetScript = new SetAssetScriptTransactionData();
            proto.SetAssetScript.AssetId = ByteString.CopyFromUtf8(tx.AssetId);
            proto.SetAssetScript.Script = ByteString.CopyFromUtf8(tx.Script);
        }
    }
}