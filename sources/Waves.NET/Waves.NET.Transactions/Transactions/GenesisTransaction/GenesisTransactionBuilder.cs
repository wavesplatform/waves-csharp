using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class GenesisTransactionBuilder : TransactionBuilder<GenesisTransactionBuilder, GenesisTransaction>
    {
        public GenesisTransactionBuilder() : base(GenesisTransaction.LatestVersion, 0, GenesisTransaction.TYPE) { }

        public GenesisTransactionBuilder(Address recipient, long amount) : this()
        {
            Transaction.Recipient = recipient;
            Transaction.Amount = amount;
        }

        public static GenesisTransactionBuilder Params(Address recipient, long amount)
        {
            return new GenesisTransactionBuilder(recipient, amount);
        }
    }
}