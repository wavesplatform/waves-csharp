using Waves.NET.Blockchain.ReturnTypes;

namespace Waves.NET.Blockchain
{
    public interface IBlockchainSection
    {
        BlockchainRewards GetBlockchainRewards();
        BlockchainRewards GetBlockchainRewards(int height);
    }
}