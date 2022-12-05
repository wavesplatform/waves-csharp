using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class IssueTransactionBuilder : TransactionBuilder<IssueTransactionBuilder, IssueTransaction>
    {
        public IssueTransactionBuilder() : base(IssueTransaction.LatestVersion, IssueTransaction.MinFee, IssueTransaction.TYPE) { }

        public IssueTransactionBuilder(string name, long quantity, int decimals, string? description = "", bool reissuable = true, Base64s? script = null) : this()
        {
            Transaction.Name = name ?? "";
            Transaction.Quantity = quantity;
            Transaction.Reissuable = reissuable;
            Transaction.Decimals = decimals;
            Transaction.Description = description ?? "";
            Transaction.Script = script;
        }

        public static IssueTransactionBuilder Params(string name, long quantity, int decimals, string? description = "", bool reissuable = true, Base64s? script = null)
        {
            return new IssueTransactionBuilder(name, quantity, decimals, description, reissuable, script);
        }
    }
}