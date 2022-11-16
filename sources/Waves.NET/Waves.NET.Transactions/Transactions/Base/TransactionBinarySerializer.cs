using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public abstract class TransactionBinarySerializer : ITransactionBinarySerializer
    {
        protected abstract IList<int> SupportedVersions { get; }

        protected abstract void SerializeInner(TransactionProto proto, Transaction transaction);

        public byte[] Serialize(Transaction transaction)
        {
            if(SupportedVersions.Any(x => x == transaction.Version))
            {
                return SerializeInner(transaction);
            }

            throw new NotSupportedException($"Ver.{transaction.Version} of a transaction is not supported ({BurnTransaction.LatestVersion} or less).");
        }

        private byte[] SerializeInner(Transaction transaction)
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
