using Waves.NET.Addresses;
using Waves.NET.Aliases;
using Waves.NET.Assets;
using Waves.NET.Blockchain;
using Waves.NET.Blocks;
using Waves.NET.Debug;
using Waves.NET.Leasing;
using Waves.NET.Node;
using Waves.NET.Transactions;
using Waves.NET.Utils;

namespace Waves.NET
{
    public interface INode : IAddressesSection, IAliasesSection, IAssetsSection, IBlockchainSection, IBlocksSection,
        IDebugSection, ILeasingSection, INodeSection, ITransactionsSection, IUtilsSection
    {
        byte ChainId { get; }
    }
}
