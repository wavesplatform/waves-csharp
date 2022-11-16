namespace Waves.NET.Transactions
{
    public class CreateAliasTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 3 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (ICreateAliasTransaction)transaction;
            proto.CreateAlias = new CreateAliasTransactionData();
            proto.CreateAlias.Alias = tx.Alias;
        }
    }
}
