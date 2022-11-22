using Waves.NET.ReturnTypes;

namespace Waves.NET.Sections
{
    public interface IBlockchainSection
    {
        /// <summary>
        /// Get current status of block reward
        /// </summary>
        /// <returns></returns>
        BlockchainRewards GetBlockchainRewards();

        /// <summary>
        /// Get status of block reward at height
        /// </summary>
        /// <param name="height">Target block height</param>
        /// <returns></returns>
        BlockchainRewards GetBlockchainRewards(int height);
    }
}