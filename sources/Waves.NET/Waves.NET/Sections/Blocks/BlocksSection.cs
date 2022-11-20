using Waves.NET.Blocks.ReturnTypes;

namespace Waves.NET.Blocks
{
    public class BlocksSection : SectionBase, IBlocksSection
    {
        public BlocksSection(HttpClient httpClient) : base(httpClient, "blocks") { }

        public Block GetBlock(string blockId)
        {
            return PublicRequest<Block>(HttpMethod.Get, $"{blockId}");
        }

        public ICollection<Block> GetBlocksGeneratedBy(string generator, int fromHeight, int toHeight)
        {
            return PublicRequest<ICollection<Block>>(HttpMethod.Get, $"address/{generator}/{fromHeight}/{toHeight}");
        }

        public Block GetBlock(int height)
        {
            return PublicRequest<Block>(HttpMethod.Get, $"at/{height}");
        }

        public long GetBlocksDelay(string startBlockId, int blocksNum)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"delay/{startBlockId}/{blocksNum}").delay;
        }

        public BlockHeader GetBlockHeaders(string blockId)
        {
            return PublicRequest<BlockHeader>(HttpMethod.Get, $"headers/{blockId}");
        }

        public BlockHeader GetBlockHeaders(int height)
        {
            return PublicRequest<BlockHeader>(HttpMethod.Get, $"headers/at/{height}");
        }

        public BlockHeader GetLastBlockHeaders()
        {
            return PublicRequest<BlockHeader>(HttpMethod.Get, $"headers/last");
        }

        public ICollection<BlockHeader> GetBlocksHeaders(int fromHeight, int toHeight)
        {
            return PublicRequest<ICollection<BlockHeader>>(HttpMethod.Get, $"headers/seq/{fromHeight}/{toHeight}");
        }

        public int GetHeight()
        {
            return PublicRequest<dynamic>(HttpMethod.Get, "height").height;
        }

        public int GetBlockHeight(string blockId)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"height/{blockId}").height;
        }

        public int GetBlockHeight(long timestamp)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"heightByTimestamp/{timestamp}").height;
        }

        public Block GetLastBlock()
        {
            return PublicRequest<Block>(HttpMethod.Get, $"last");
        }

        public Block GetGenesisBlock()
        {
            return GetBlock(1);
        }

        public ICollection<Block> GetBlocks(int fromHeight, int toHeight)
        {
            return PublicRequest<ICollection<Block>>(HttpMethod.Get, $"seq/{fromHeight}/{toHeight}");
        }
    }
}