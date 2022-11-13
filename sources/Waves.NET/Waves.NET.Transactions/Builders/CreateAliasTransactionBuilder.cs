namespace Waves.NET.Transactions.Builders
{
    public class CreateAliasTransactionBuilder : TransactionBuilder<CreateAliasTransactionBuilder, CreateAliasTransaction>
    {
        public CreateAliasTransactionBuilder() :
            base(CreateAliasTransaction.LatestVersion, CreateAliasTransaction.MinFee, CreateAliasTransaction.TYPE) { }

        public CreateAliasTransactionBuilder(string alias) : this()
        {
            Transaction.Alias = alias;
        }

        public static CreateAliasTransactionBuilder Data(string alias)
        {
            return new CreateAliasTransactionBuilder(alias);
        }

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (ICreateAliasTransaction)Transaction;
            proto.CreateAlias = new CreateAliasTransactionData();
            proto.CreateAlias.Alias = tx.Alias;
        }
    }
}