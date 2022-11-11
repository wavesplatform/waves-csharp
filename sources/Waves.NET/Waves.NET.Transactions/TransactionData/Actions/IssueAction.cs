namespace Waves.NET.Transactions.Actions
{
    public record IssueAction
    {
        public string AssetId { get; init; } = null!;
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public int Decimals { get; init; }
        public bool IsReissuable { get; init; }
        public string CompiledScript { get; init; } = null!;
    }
}