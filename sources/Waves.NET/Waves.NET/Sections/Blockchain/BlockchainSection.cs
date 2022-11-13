using Waves.NET.Blockchain.ReturnTypes;

namespace Waves.NET.Blockchain
{
    public class BlockchainSection : SectionBase, IBlockchainSection
    {
        public BlockchainSection(HttpClient httpClient) : base(httpClient, "blockchain") { }

        /// <summary>
        /// Get current status of block reward
        /// </summary>
        /// <returns></returns>
        public BlockchainRewards GetBlockchainRewards()
        {
            return PublicRequest<BlockchainRewards>(HttpMethod.Get, "rewards");
        }

        /// <summary>
        /// Get status of block reward at height
        /// </summary>
        /// <param name="height">Target block height</param>
        /// <returns></returns>
        public BlockchainRewards GetBlockchainRewards(int height)
        {
            return PublicRequest<BlockchainRewards>(HttpMethod.Get, $"rewards/{height}");
        }
    }
}