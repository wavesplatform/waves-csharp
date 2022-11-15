using Waves.NET.Transactions.Crypto;

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

        public static CreateAliasTransactionBuilder Data(Alias alias)
        {
            return new CreateAliasTransactionBuilder(alias);
        }
    }
}