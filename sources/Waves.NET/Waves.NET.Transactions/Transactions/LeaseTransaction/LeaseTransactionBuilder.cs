namespace Waves.NET.Transactions
{
    public class LeaseTransactionBuilder : TransactionBuilder<LeaseTransactionBuilder, LeaseTransaction>
    {
        public LeaseTransactionBuilder() : base(LeaseTransaction.LatestVersion, LeaseTransaction.MinFee, LeaseTransaction.TYPE) { }

        public LeaseTransactionBuilder(string recipient, long amount) : this()
        {
            Transaction.Recipient = recipient;
            Transaction.Amount = amount;
        }

        public static LeaseTransactionBuilder Data(string recipient, long amount)
        {
            return new LeaseTransactionBuilder(recipient, amount);
        }
    }
}