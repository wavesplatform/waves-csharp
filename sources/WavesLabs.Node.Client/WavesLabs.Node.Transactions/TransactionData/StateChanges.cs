using WavesLabs.Node.Transactions.Actions;

namespace WavesLabs.Node.Transactions
{
    public class StateChanges : IEquatable<StateChanges?>
    {
        public ICollection<EntryData> Data { get; init; } = null!;
        public ICollection<TransferAction> Transfers { get; init; } = null!;
        public ICollection<IssueAction> Issues { get; init; } = null!;
        public ICollection<ReissueAction> Reissues { get; init; } = null!;
        public ICollection<BurnAction> Burns { get; init; } = null!;
        public ICollection<SponsorFeeAction> SponsorFees { get; init; } = null!;
        public ICollection<LeaseInfo> Leases { get; init; } = null!;
        public ICollection<LeaseInfo> LeaseCancels { get; init; } = null!;
        public ICollection<InvokeAction> Invokes { get; init; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as StateChanges is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as StateChanges);
        }

        public bool Equals(StateChanges? other)
        {
            return other is not null &&
                   (Data is null && other.Data is null || Data.SequenceEqual(other.Data)) &&
                   (Transfers is null && other.Transfers is null || Transfers.SequenceEqual(other.Transfers)) &&
                   (Issues is null && other.Issues is null || Issues.SequenceEqual(other.Issues)) &&
                   (Reissues is null && other.Reissues is null || Reissues.SequenceEqual(other.Reissues)) &&
                   (Burns is null && other.Burns is null || Burns.SequenceEqual(other.Burns)) &&
                   (SponsorFees is null && other.SponsorFees is null || SponsorFees.SequenceEqual(other.SponsorFees)) &&
                   (Leases is null && other.Leases is null || Leases.SequenceEqual(other.Leases)) &&
                   (LeaseCancels is null && other.LeaseCancels is null || LeaseCancels.SequenceEqual(other.LeaseCancels)) &&
                   (Invokes is null && other.Invokes is null || Invokes.SequenceEqual(other.Invokes));
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Data.CalcHashCode());
            hash.Add(Transfers.CalcHashCode());
            hash.Add(Issues.CalcHashCode());
            hash.Add(Reissues.CalcHashCode());
            hash.Add(Burns.CalcHashCode());
            hash.Add(SponsorFees.CalcHashCode());
            hash.Add(Leases.CalcHashCode());
            hash.Add(LeaseCancels.CalcHashCode());
            hash.Add(Invokes.CalcHashCode());
            return hash.ToHashCode();
        }

        public static bool operator ==(StateChanges? left, StateChanges? right) => EqualityComparer<StateChanges>.Default.Equals(left, right);
        public static bool operator !=(StateChanges? left, StateChanges? right) => !(left == right);
    }
}