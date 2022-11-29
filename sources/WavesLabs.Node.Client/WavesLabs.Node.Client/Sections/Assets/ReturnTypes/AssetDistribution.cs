using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Client.ReturnTypes
{
    public record AssetDistribution
    {
        public bool HasNext { get; init; }
        public Address LastItem { get; init; } = null!;
        public IDictionary<string, long> Items { get; init; } = null!;
    }
}
