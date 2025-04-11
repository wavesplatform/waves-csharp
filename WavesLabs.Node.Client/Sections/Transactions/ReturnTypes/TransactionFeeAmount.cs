namespace WavesLabs.Node.Client.ReturnTypes
{
    public record TransactionFeeAmount
    {
        public string FeeAssetId { get; init; } = null!;
        public long FeeAmount { get; init; }
    }
}