using Google.Protobuf;
using Waves;

namespace WavesLabs.Node.Transactions
{
    public class BurnTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 3 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IBurnTransaction)transaction;
            proto.Burn = new BurnTransactionData();
            proto.Burn.AssetAmount = new AmountProto
            {
                Amount_ = tx.Amount,
                AssetId = tx.AssetId is null ? ByteString.Empty : ByteString.CopyFrom(tx.AssetId)
            };
        }
    }
}
