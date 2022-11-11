using Waves.NET.Blocks.ReturnTypes;

namespace Waves.NET.Blocks
{
    public interface IBlocksSection
    {
        Block GetBlock(int height);
        Block GetBlock(string blockId);
        BlockHeaders GetBlockHeaders(int height);
        BlockHeaders GetBlockHeaders(string blockId);
        int GetBlockHeight(long timestamp);
        int GetBlockHeight(string blockId);
        ICollection<Block> GetBlocks(int fromHeight, int toHeight);
        long GetBlocksDelay(string startBlockId, int blocksNum);
        ICollection<Block> GetBlocksGeneratedBy(string generator, int fromHeight, int toHeight);
        ICollection<BlockHeaders> GetBlocksHeaders(int fromHeight, int toHeight);
        int GetHeight();
        Block GetLastBlock();
        BlockHeaders GetLastBlockHeaders();
    }
}