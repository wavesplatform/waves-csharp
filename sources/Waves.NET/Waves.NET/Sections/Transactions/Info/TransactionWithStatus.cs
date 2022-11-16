namespace Waves.NET.Transactions.Info
{
    public abstract class TransactionWithStatus
    {
        public virtual Transaction Transaction { get; init; }

        public ApplicationStatus ApplicationStatus { get; init; }

        public TransactionWithStatus(Transaction transaction, ApplicationStatus? applicationStatus)
        {
            if(transaction is null) throw new ArgumentNullException("Argument 'transaction' cannot be null.");

            Transaction = transaction;
            ApplicationStatus = applicationStatus is null || applicationStatus == ApplicationStatus.NotSet
                ? ApplicationStatus.Succeeded
                : applicationStatus.Value;
        }
    }
}
