using Google.Protobuf;
using Waves;

namespace WavesLabs.Node.Transactions
{
    public class SponsorFeeTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 2 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (ISponsorFeeTransaction)transaction;
            proto.SponsorFee = new SponsorFeeTransactionData();
            proto.SponsorFee.MinFee = new AmountProto
            {
                Amount_ = tx.MinSponsoredAssetFee,
                AssetId = tx.AssetId is null ? ByteString.Empty : ByteString.CopyFrom(tx.AssetId)
            };
        }
    }
}
