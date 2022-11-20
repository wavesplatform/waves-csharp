using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class InvokeScriptTransaction : Transaction, IInvokeScriptTransaction, IEquatable<InvokeScriptTransaction?>
    {
        public const int TYPE = 16;
        public const int LatestVersion = 2;
        public const int MinFee = 500000;

        public IRecipient DApp { get; set; } = null!;
        public ICollection<Payment> Payment { get; set; } = null!;
        public Call Call { get; set; } = null!;
        public StateChanges StateChanges { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as InvokeScriptTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as InvokeScriptTransaction);
        }

        public bool Equals(InvokeScriptTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<IRecipient>.Default.Equals(DApp, other.DApp) &&
                   EqualityComparer<Call>.Default.Equals(Call, other.Call) &&
                   EqualityComparer<StateChanges>.Default.Equals(StateChanges, other.StateChanges) &&
                   (Payment is null && other.Payment is null || Payment.SequenceEqual(other.Payment));
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), DApp, Payment, Call, StateChanges);
        public static bool operator ==(InvokeScriptTransaction? left, InvokeScriptTransaction? right) =>
            EqualityComparer<InvokeScriptTransaction>.Default.Equals(left, right);
        public static bool operator !=(InvokeScriptTransaction? left, InvokeScriptTransaction? right) => !(left == right);
    }
}