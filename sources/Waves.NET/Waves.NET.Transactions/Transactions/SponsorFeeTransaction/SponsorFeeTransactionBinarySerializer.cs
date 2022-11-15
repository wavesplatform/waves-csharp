using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class SponsorFeeTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Signature }, { 2, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (ISponsorFeeTransaction)transaction;
            proto.SponsorFee = new SponsorFeeTransactionData();
            proto.SponsorFee.MinFee = new AmountProto
            {
                Amount_ = tx.MinSponsoredAssetFee,
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
