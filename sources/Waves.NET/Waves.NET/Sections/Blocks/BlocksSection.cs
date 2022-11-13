using Waves.NET.Blocks.ReturnTypes;

namespace Waves.NET.Blocks
{
    public class BlocksSection : SectionBase, IBlocksSection
    {
        public BlocksSection(HttpClient httpClient) : base(httpClient, "blocks") { }

        /// <summary>
        /// Get a block by its ID
        /// </summary>
        /// <param name="blockId">Block ID base58 encoded</param>
        /// <returns>Block by ID</returns>
        public Block GetBlock(string blockId)
        {
            return PublicRequest<Block>(HttpMethod.Get, $"{blockId}");
        }

        /// <summary>
        /// Get a list of blocks forged by a given address. Max range {from}-{to} is 100 blocks
        /// </summary>
        /// <param name="generator">Address base58 encoded</param>
        /// <param name="fromHeight">Start block height</param>
        /// <param name="toHeight">End block height</param>
        /// <returns>Blocks forged by address</returns>
        public ICollection<Block> GetBlocksGeneratedBy(string generator, int fromHeight, int toHeight)
        {
            return PublicRequest<ICollection<Block>>(HttpMethod.Get, $"address/{generator}/{fromHeight}/{toHeight}");
        }

        /// <summary>
        /// Get a block at a given height
        /// </summary>
        /// <param name="height">Block height</param>
        /// <returns>Block at height</returns>
        public Block GetBlock(int height)
        {
            return PublicRequest<Block>(HttpMethod.Get, $"at/{height}");
        }

        /// <summary>
        /// Average delay in milliseconds between last <c>blockNum</c> blocks starting from block with <c>id</c>
        /// </summary>
        /// <param name="startBlockId">Block ID base58 encoded</param>
        /// <param name="blocksNum">Number of blocks to count delay</param>
        /// <returns>Average block delay</returns>
        public long GetBlocksDelay(string startBlockId, int blocksNum)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"delay/{startBlockId}/{blocksNum}").delay;
        }

        /// <summary>
        /// Get headers of a given block
        /// </summary>
        /// <param name="blockId">Block ID base58 encoded</param>
        /// <returns>Block headers by ID</returns>
        public BlockHeaders GetBlockHeaders(string blockId)
        {
            return PublicRequest<BlockHeaders>(HttpMethod.Get, $"headers/{blockId}");
        }

        /// <summary>
        /// Get block headers at a given height
        /// </summary>
        /// <param name="height">Block height</param>
        /// <returns>Block headers at height</returns>
        public BlockHeaders GetBlockHeaders(int height)
        {
            return PublicRequest<BlockHeaders>(HttpMethod.Get, $"headers/at/{height}");
        }

        /// <summary>
        /// Get headers of the block at the current blockchain height
        /// </summary>
        /// <returns>Last block headers</returns>
        public BlockHeaders GetLastBlockHeaders()
        {
            return PublicRequest<BlockHeaders>(HttpMethod.Get, $"headers/last");
        }

        /// <summary>
        /// Get block headers at a given range of heights. Max range {from}-{to} is 100 blocks
        /// </summary>
        /// <param name="fromHeight">Start block height</param>
        /// <param name="toHeight">End block height</param>
        /// <returns>Block headers at range</returns>
        public ICollection<BlockHeaders> GetBlocksHeaders(int fromHeight, int toHeight)
        {
            return PublicRequest<ICollection<BlockHeaders>>(HttpMethod.Get, $"headers/seq/{fromHeight}/{toHeight}");
        }

        /// <summary>
        /// Get the current blockchain height
        /// </summary>
        /// <returns>Blockchain height</returns>
        public int GetHeight()
        {
            return PublicRequest<dynamic>(HttpMethod.Get, "height").height;
        }

        /// <summary>
        /// Get the height of a block by its ID
        /// </summary>
        /// <param name="blockId">Block ID base58 encoded</param>
        /// <returns>Block height</returns>
        public int GetBlockHeight(string blockId)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"height/{blockId}").height;
        }

        /// <summary>
        /// Get height of the most recent block such that its timestamp does not exceed the given <c>{timestamp}</c>.
        /// </summary>
        /// <param name="timestamp">Timestamp</param>
        /// <returns>Blockchain height for timestamp</returns>
        public int GetBlockHeight(long timestamp)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"heightByTimestamp/{timestamp}").height;
        }

        /// <summary>
        /// Get the block at the current blockchain height
        /// </summary>
        /// <returns>Last block</returns>
        public Block GetLastBlock()
        {
            return PublicRequest<Block>(HttpMethod.Get, $"last");
        }

        /// <summary>
        /// Get blocks at a given range of heights. Max range {from}-{to} is 100 blocks
        /// </summary>
        /// <param name="fromHeight">Start block height</param>
        /// <param name="toHeight">End block height</param>
        /// <returns>Block range</returns>
        public ICollection<Block> GetBlocks(int fromHeight, int toHeight)
        {
            return PublicRequest<ICollection<Block>>(HttpMethod.Get, $"seq/{fromHeight}/{toHeight}");
        }

    }
}