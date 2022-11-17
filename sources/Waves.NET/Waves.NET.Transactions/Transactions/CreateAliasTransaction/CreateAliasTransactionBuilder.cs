using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class CreateAliasTransactionBuilder : TransactionBuilder<CreateAliasTransactionBuilder, CreateAliasTransaction>
    {
        public CreateAliasTransactionBuilder() :
            base(CreateAliasTransaction.LatestVersion, CreateAliasTransaction.MinFee, CreateAliasTransaction.TYPE) { }

        public CreateAliasTransactionBuilder(Alias alias) : this()
        {
            Transaction.Alias = alias;
        }

        public static CreateAliasTransactionBuilder Params(Alias alias)
        {
            return new CreateAliasTransactionBuilder(alias);
        }
    }
}