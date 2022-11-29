using Google.Protobuf;
using Waves;

namespace WavesLabs.Node.Transactions
{
    public abstract class TransactionBinarySerializer : ITransactionBinarySerializer
    {
        protected abstract IList<int> SupportedVersions { get; }

        protected abstract void SerializeInner(TransactionProto proto, Transaction transaction);

        public byte[] Serialize(Transaction transaction)
        {
            if(SupportedVersions.Any(x => x == transaction.Version))
            {
                return BuildProtoBytes(transaction);
            }

            throw new NotSupportedException($"Ver.{transaction.Version} of a transaction is not supported.");
        }

        protected virtual byte[] BuildProtoBytes(Transaction transaction)
        {
            var proto = new TransactionProto();
            proto.ChainId = transaction.ChainId;
            proto.SenderPublicKey = ByteString.CopyFrom(transaction.SenderPublicKey);
            proto.Fee = new AmountProto { AssetId = ByteString.Empty, Amount_ = transaction.Fee };
            proto.Timestamp = transaction.Timestamp;
            proto.Version = transaction.Version;

            SerializeInner(proto, transaction);

            return proto.ToByteArray();
        }
    }
}
