using Google.Protobuf;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions
{
    public class GenesisTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (IGenesisTransaction)transaction;

            proto.Genesis.RecipientAddress = tx.Recipient.Type == Address.TYPE
                ? ByteString.CopyFrom(((Address)tx.Recipient).PublicKeyHash)
                : ByteString.CopyFromUtf8(((Alias)tx.Recipient).Name);

            proto.Genesis.Amount = tx.Amount;
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