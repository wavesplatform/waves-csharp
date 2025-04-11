namespace WavesLabs.Node.Client.ReturnTypes
{
    public record IssueTransactionBalanceDetails
    {
        public string AssetId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public long Quantity { get; set; }
        public bool Reissuable { get; set; }
        public int Decimals { get; set; }
        public string Description { get; set; } = null!;
        public string Script { get; set; } = null!;
    }
}
