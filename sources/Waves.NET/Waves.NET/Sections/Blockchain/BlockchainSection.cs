using Waves.NET.ReturnTypes;

namespace Waves.NET.Sections
{
    public class BlockchainSection : SectionBase, IBlockchainSection
    {
        public BlockchainSection(HttpClient httpClient) : base(httpClient, "blockchain") { }

        public BlockchainRewards GetBlockchainRewards()
        {
            return PublicRequest<BlockchainRewards>(HttpMethod.Get, "rewards");
        }

        public BlockchainRewards GetBlockchainRewards(int height)
        {
            return PublicRequest<BlockchainRewards>(HttpMethod.Get, $"rewards/{height}");
        }
    }
}