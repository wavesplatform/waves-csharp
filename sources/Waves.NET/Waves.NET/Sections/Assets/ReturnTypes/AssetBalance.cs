namespace Waves.NET.Assets
{
    public record AssetBalance
    {
        public string Address { get; set; } = null!;
        public ICollection<AssetBalanceAndDetails> Balances { get; set; } = null!;
    }
}
