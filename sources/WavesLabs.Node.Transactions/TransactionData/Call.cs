namespace WavesLabs.Node.Transactions
{
    public class Call : IEquatable<Call?>
    {
        public string Function { get; init; } = "";
        public ICollection<CallArg> Args { get; init; } = new List<CallArg>();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as Call is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as Call);
        }

        public bool Equals(Call? other)
        {
            return other is not null &&
                   Function == other.Function &&
                   Args.SequenceEqual(other.Args);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Function, Args.CalcHashCode());
        }

        public static bool operator ==(Call? left, Call? right) => EqualityComparer<Call>.Default.Equals(left, right);
        public static bool operator !=(Call? left, Call? right) => !(left == right);
    }
}