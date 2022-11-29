using Newtonsoft.Json;
using WavesLabs.Node.Client.Json;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions.Info
{
    [JsonConverter(typeof(TransactionInfoJsonConverter))]
    public abstract class TransactionInfo : TransactionWithStatus, IEquatable<TransactionInfo?>
    {
        public int Height { get; init; }

        public TransactionInfo(Transaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus)
        {
            Height = height == 0 && Transaction.Type == GenesisTransaction.TYPE ? 1 : height;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as TransactionInfo is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as TransactionInfo);
        }

        public bool Equals(TransactionInfo? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   Height == other.Height;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Height);
        public static bool operator ==(TransactionInfo? left, TransactionInfo? right) => EqualityComparer<TransactionInfo>.Default.Equals(left, right);
        public static bool operator !=(TransactionInfo? left, TransactionInfo? right) => !(left == right);
    }
}
