using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class UpdateAssetInfoTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 1 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IUpdateAssetInfoTransaction)transaction;
            proto.UpdateAssetInfo = new UpdateAssetInfoTransactionData();
            proto.UpdateAssetInfo.AssetId = tx.AssetId is null ? ByteString.Empty : ByteString.CopyFrom(tx.AssetId);
            proto.UpdateAssetInfo.Name = tx.Name;
            proto.UpdateAssetInfo.Description = tx.Description;
        }
    }
}
