namespace Waves.NET.Assets.ReturnTypes
{
    public record AssetBalance
    {
        public string Address { get; set; } = null!;
        public ICollection<AssetBalanceAndDetails> Balances { get; set; } = null!;
    }
}
