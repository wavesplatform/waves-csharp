namespace Waves.NET.Transactions
{
    public class DataTransaction : Transaction, IDataTransaction, IEquatable<DataTransaction?>
    {
        public const int TYPE = 12;
        public const int LatestVersion = 2;
        public const int MinFee = 100000;

        public ICollection<EntryData> Data { get; set; } = null!;

        public DataTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as DataTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as DataTransaction);
        }

        public bool Equals(DataTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<ICollection<EntryData>>.Default.Equals(Data, other.Data);
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Data.CalcHashCode());
        public static bool operator ==(DataTransaction? left, DataTransaction? right) => EqualityComparer<DataTransaction>.Default.Equals(left, right);
        public static bool operator !=(DataTransaction? left, DataTransaction? right) => !(left == right);
    }
}