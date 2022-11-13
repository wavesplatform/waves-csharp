using Google.Protobuf;

namespace Waves.NET.Transactions.Builders
{
    public class IssueTransactionBuilder : TransactionBuilder<IssueTransactionBuilder, IssueTransaction>
    {
        public IssueTransactionBuilder() : base(IssueTransaction.LatestVersion, IssueTransaction.MinFee, IssueTransaction.TYPE) { }

        public IssueTransactionBuilder(string assetId, string name, long quantity, int decimals, bool reissuable, string description, string script) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Name = name;
            Transaction.Quantity = quantity;
            Transaction.Reissuable = reissuable;
            Transaction.Decimals = decimals;
            Transaction.Description = description;
            Transaction.Script = script;
        }

        public static IssueTransactionBuilder Data(string assetId, string name, long quantity, int decimals, bool reissuable, string description, string script)
        {
            return new IssueTransactionBuilder(assetId, name, quantity, decimals, reissuable, description, script);
        }

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (IIssueTransaction)Transaction;
            proto.Issue = new IssueTransactionData();
            proto.Issue.Name = tx.Name;
            proto.Issue.Description = tx.Description;
            proto.Issue.Amount = tx.Amount;
            proto.Issue.Decimals = tx.Decimals;
            proto.Issue.Reissuable = tx.Reissuable;
            proto.Issue.Script = ByteString.CopyFromUtf8(tx.Script);
        }
    }
}