using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class IssueTransactionBuilder : TransactionBuilder<IssueTransactionBuilder, IssueTransaction>
    {
        public IssueTransactionBuilder() : base(IssueTransaction.LatestVersion, IssueTransaction.MinFee, IssueTransaction.TYPE) { }

        public IssueTransactionBuilder(string name, long quantity, int decimals) : this()
        {
            Transaction.Name = name ?? "";
            Transaction.Quantity = quantity;
            Transaction.Reissuable = true;
            Transaction.Decimals = decimals;
            Transaction.Description = "";
            Transaction.Script = null;
        }

        public static IssueTransactionBuilder Params(string name, long quantity, int decimals)
        {
            return new IssueTransactionBuilder(name, quantity, decimals);
        }

        public IssueTransactionBuilder SetReissuable(bool reissuable)
        {
            Transaction.Reissuable = reissuable;
            return this;
        }

        public IssueTransactionBuilder SetDescription(string description)
        {
            Transaction.Description = description;
            return this;
        }

        public IssueTransactionBuilder SetScript(Base64s? script)
        {
            Transaction.Script = script;
            return this;
        }
    }
}