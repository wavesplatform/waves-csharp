using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class CreateAliasTransactionBuilder : TransactionBuilder<CreateAliasTransactionBuilder, CreateAliasTransaction>
    {
        public CreateAliasTransactionBuilder() :
            base(CreateAliasTransaction.LatestVersion, CreateAliasTransaction.MinFee, CreateAliasTransaction.TYPE) { }

        public CreateAliasTransactionBuilder(string alias) : this()
        {
            Transaction.Alias = alias;
        }

        public static CreateAliasTransactionBuilder Params(string alias)
        {
            return new CreateAliasTransactionBuilder(alias);
        }
    }
}