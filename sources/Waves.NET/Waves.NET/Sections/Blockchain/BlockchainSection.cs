using Waves.NET.Blockchain.ReturnTypes;

namespace Waves.NET.Blockchain
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