namespace Waves.NET.Transactions.Actions
{
    public record LeaseCancelAction
    {
        public string LeaseId { get; init; } = null!;
    }
}