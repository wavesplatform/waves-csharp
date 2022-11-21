using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions.Actions
{
    public record LeaseCancelAction
    {
        public Base58s LeaseId { get; init; } = null!;
    }
}