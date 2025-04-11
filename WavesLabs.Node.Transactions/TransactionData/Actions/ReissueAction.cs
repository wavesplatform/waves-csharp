namespace WavesLabs.Node.Transactions.Actions
{
    public record ReissueAction
    {
        public string AssetId { get; init; } = null!;
        public bool IsReissuable { get; init; }
        public long Quantity { get; init; }
    }
}