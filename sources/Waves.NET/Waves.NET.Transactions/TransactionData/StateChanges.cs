using Waves.NET.Transactions.Actions;

namespace Waves.NET.Transactions
{
    public record StateChanges
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
    }
}