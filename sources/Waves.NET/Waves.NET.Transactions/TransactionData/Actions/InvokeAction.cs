namespace Waves.NET.Transactions.Actions
{
    public class InvokeAction : IEquatable<InvokeAction?>
    {
        public string DApp { get; init; } = "";
        public string Function { get; init; } = "";
        public ICollection<Amount> Payments { get; init; } = new List<Amount>();
        public object Error { get; init; }
        public StateChanges StateChanges { get; init; }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as InvokeAction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as InvokeAction);
        }

        public bool Equals(InvokeAction? other)
        {
            return other is not null &&
                   DApp == other.DApp &&
                   Function == other.Function &&
                   (Payments is null && other.Payments is null || Payments.SequenceEqual(other.Payments)) &&
                   EqualityComparer<object>.Default.Equals(Error, other.Error) &&
                   EqualityComparer<StateChanges>.Default.Equals(StateChanges, other.StateChanges);
        }

        public override int GetHashCode() => HashCode.Combine(DApp, Function, Payments.CalcHashCode(), Error, StateChanges);
        public static bool operator ==(InvokeAction? left, InvokeAction? right) => EqualityComparer<InvokeAction>.Default.Equals(left, right);
        public static bool operator !=(InvokeAction? left, InvokeAction? right) => !(left == right);
    }
}