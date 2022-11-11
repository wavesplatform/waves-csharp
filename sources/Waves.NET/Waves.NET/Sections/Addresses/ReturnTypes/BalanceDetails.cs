namespace Waves.NET.Addresses.ReturnTypes
{
    public record BalanceDetails
    {
        public string Address { get; init; } = null!;
        public long Regular { get; init; }
        public long Generating { get; init; }
        public long Available { get; init; }
        public long Effective { get; init; }
    }
}
