using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public enum TransactionSchema { Unknown = 0, Signature, Proofs, Protobuf }

    public abstract class TransactionBinarySerializer : ITransactionBinarySerializer
    {
        protected abstract Dictionary<int, TransactionSchema> VersionToSchemaMap { get; }

        protected abstract void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction);
        protected abstract void SerializeToProofsSchema(BinaryWriter bw, Transaction transaction);
        protected abstract void SerializeToSignatureSchema(BinaryWriter bw, Transaction transaction);

        public byte[] Serialize(Transaction transaction)
        {
            var schema = VersionToSchemaMap.GetValueOrDefault(transaction.Version);
            switch (schema)
            {
                case TransactionSchema.Protobuf: return SerializeToProtobufSchema(transaction);
                case TransactionSchema.Proofs: return SerializeToProofs(transaction);
                case TransactionSchema.Signature: return SerializeToSignature(transaction);
                default:
                    throw new ArgumentOutOfRangeException($"Ver.{transaction.Version} of a transaction is not supported ({BurnTransaction.LatestVersion} or less).");
            }
        }

        private byte[] SerializeToSignature(Transaction transaction)
        {
            using var stream = new MemoryStream();
            using var bw = new BinaryWriter(stream);
            SerializeToSignatureSchema(bw, transaction);
            return stream.ToArray();
        }

        private byte[] SerializeToProofs(Transaction transaction)
        {
            using var stream = new MemoryStream();
            using var bw = new BinaryWriter(stream);
            SerializeToProofsSchema(bw, transaction);
            return stream.ToArray();
        }

        private byte[] SerializeToProtobufSchema(Transaction transaction)
        {
            var proto = new TransactionProto();
            proto.ChainId = transaction.ChainId;
            proto.SenderPublicKey = ByteString.CopyFrom(transaction.SenderPublicKey);
            proto.Fee = new AmountProto { AssetId = ByteString.Empty, Amount_ = transaction.Fee };
            proto.Timestamp = transaction.Timestamp;
            proto.Version = transaction.Version;
            SerializeToProtobufSchema(proto, transaction);
            return proto.ToByteArray();
        }
    }
}
