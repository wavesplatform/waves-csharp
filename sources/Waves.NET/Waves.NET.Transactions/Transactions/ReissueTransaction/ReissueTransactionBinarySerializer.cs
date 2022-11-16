using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class ReissueTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 3 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
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
    }
}