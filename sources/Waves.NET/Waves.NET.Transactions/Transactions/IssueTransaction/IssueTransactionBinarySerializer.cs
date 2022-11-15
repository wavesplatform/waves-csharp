using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class IssueTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Signature }, { 2, TransactionSchema.Proofs }, { 3, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
        {
            var tx = (IIssueTransaction)transaction;
            proto.Issue = new IssueTransactionData();
            proto.Issue.Name = tx.Name;
            proto.Issue.Description = tx.Description;
            proto.Issue.Amount = tx.Amount;
            proto.Issue.Decimals = tx.Decimals;
            proto.Issue.Reissuable = tx.Reissuable;
            proto.Issue.Script = ByteString.CopyFromUtf8(tx.Script);
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