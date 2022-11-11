namespace Waves.NET.Assets.ReturnTypes
{
    public record AssetDistribution
    {
        public bool HasNext { get; init; }
        public string Last { get; init; } = null!;
        public IDictionary<string, long> Values { get; init; } = null!;
    }
}
