namespace WavesLabs.Node.Transactions
{
    public class SetScriptTransaction : Transaction, ISetScriptTransaction, IEquatable<SetScriptTransaction?>
    {
        public const int TYPE = 13;
        public const int LatestVersion = 2;
        public const int MinFee = 1000000;

        public string Script { get; set; } = null!;

        public SetScriptTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as SetScriptTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as SetScriptTransaction);
        }

        public bool Equals(SetScriptTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   Script == other.Script;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Script);
        public static bool operator ==(SetScriptTransaction? left, SetScriptTransaction? right) => EqualityComparer<SetScriptTransaction>.Default.Equals(left, right);
        public static bool operator !=(SetScriptTransaction? left, SetScriptTransaction? right) => !(left == right);
    }
}