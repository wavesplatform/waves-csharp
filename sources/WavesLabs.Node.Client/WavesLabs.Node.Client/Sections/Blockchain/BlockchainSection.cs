using WavesLabs.Node.Client.ReturnTypes;

namespace WavesLabs.Node.Client.Sections
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