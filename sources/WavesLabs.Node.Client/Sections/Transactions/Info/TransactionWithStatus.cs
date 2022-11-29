using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions.Info
{
    public abstract class TransactionWithStatus : IEquatable<TransactionWithStatus?>
    {
        public virtual Transaction Transaction { get; init; }

        public ApplicationStatus ApplicationStatus { get; init; }

        public TransactionWithStatus(Transaction transaction, ApplicationStatus? applicationStatus)
        {
            if(transaction is null) throw new ArgumentNullException("Argument 'transaction' cannot be null.");

            Transaction = transaction;

            var appStatus = applicationStatus ?? transaction.ApplicationStatus;

            ApplicationStatus = appStatus == ApplicationStatus.NotSet ? ApplicationStatus.Succeeded : appStatus;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as TransactionWithStatus is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as TransactionWithStatus);
        }

        public bool Equals(TransactionWithStatus? other)
        {
            return other is not null &&
                   EqualityComparer<Transaction>.Default.Equals(Transaction, other.Transaction) &&
                   ApplicationStatus == other.ApplicationStatus;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Transaction, ApplicationStatus);
        }

        public static bool operator ==(TransactionWithStatus? left, TransactionWithStatus? right) => EqualityComparer<TransactionWithStatus>.Default.Equals(left, right);
        public static bool operator !=(TransactionWithStatus? left, TransactionWithStatus? right) => !(left == right);
    }
}
