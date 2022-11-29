namespace WavesLabs.Node.Transactions.Actions
{
    public record BurnAction
    {
        public string AssetId { get; init; } = "";
        public int Amount { get; init; }
    }
}