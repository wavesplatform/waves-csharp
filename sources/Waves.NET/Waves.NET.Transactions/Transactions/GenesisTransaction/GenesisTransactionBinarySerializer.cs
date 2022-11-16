using Google.Protobuf;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class GenesisTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 1 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IGenesisTransaction)transaction;
            proto.Genesis = new GenesisTransactionData
            {
                Amount = tx.Amount,
                RecipientAddress = tx.Recipient.Type == Address.TYPE
                    ? ByteString.CopyFrom(((Address)tx.Recipient).PublicKeyHash)
                    : ByteString.CopyFromUtf8(((Alias)tx.Recipient).Name)
            };
        }
    }
}