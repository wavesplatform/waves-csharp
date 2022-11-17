using Google.Protobuf;

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
                RecipientAddress = ByteString.CopyFrom(tx.Recipient.PublicKeyHash)
            };
        }
    }
}