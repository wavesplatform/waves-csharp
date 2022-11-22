namespace Waves.NET.ReturnTypes
{
    public record TransactionFeeAmount
    {
        public string FeeAssetId { get; init; } = null!;
        public long FeeAmount { get; init; }
    }
}