namespace Waves.NET.Transactions.Info
{
    public abstract class TransactionWithStatus
    {
        public virtual Transaction Transaction { get; init; }

        public string ApplicationStatus { get; init; }

        public TransactionWithStatus(Transaction transaction, string? applicationStatus)
        {
            if(transaction is null) throw new ArgumentNullException("Argument 'transaction' cannot be null.");

            Transaction = transaction;
            ApplicationStatus = string.IsNullOrWhiteSpace(applicationStatus) ? "succeeded" : applicationStatus;
        }
    }
}
