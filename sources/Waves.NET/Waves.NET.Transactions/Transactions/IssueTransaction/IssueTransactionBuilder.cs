using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class IssueTransactionBuilder : TransactionBuilder<IssueTransactionBuilder, IssueTransaction>
    {
        public IssueTransactionBuilder() : base(IssueTransaction.LatestVersion, IssueTransaction.MinFee, IssueTransaction.TYPE) { }

        public IssueTransactionBuilder(Base58s assetId, string name, long quantity, int decimals, bool reissuable, string description, string? script) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Name = name;
            Transaction.Quantity = quantity;
            Transaction.Reissuable = reissuable;
            Transaction.Decimals = decimals;
            Transaction.Description = description;
            Transaction.Script = script;
        }

        public static IssueTransactionBuilder Params(Base58s assetId, string name, long quantity, int decimals, bool reissuable, string description, string? script)
        {
            return new IssueTransactionBuilder(assetId, name, quantity, decimals, reissuable, description, script);
        }
    }
}