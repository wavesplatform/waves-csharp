using Google.Protobuf;
using Waves;

namespace WavesLabs.Node.Transactions
{
    public class SetAssetScriptTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 2 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (ISetAssetScriptTransaction)transaction;
            proto.SetAssetScript = new SetAssetScriptTransactionData();
            proto.SetAssetScript.AssetId = tx.AssetId is null ? ByteString.Empty : ByteString.CopyFrom(tx.AssetId);
            proto.SetAssetScript.Script = ByteString.FromBase64(tx.Script);
        }
    }
}