using Google.Protobuf;
using Waves;

namespace WavesLabs.Node.Transactions
{
    public class IssueTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 3 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IIssueTransaction)transaction;
            proto.Issue = new IssueTransactionData
            {
                Name = tx.Name,
                Description = tx.Description,
                Amount = tx.Quantity,
                Decimals = tx.Decimals,
                Reissuable = tx.Reissuable,
                Script = tx.Script is null ? ByteString.Empty : ByteString.FromBase64(tx.Script)
            };
        }
    }
}