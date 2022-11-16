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
    public interface INodeClient
    {
        byte ChainId { get; }

        IAddressesSection Addresses { get; }
        IAliasSection Alias { get; }
        IAssetsSection Assets { get; }
        IBlockchainSection Blockchain { get; }
        IBlocksSection Blocks { get; }
        IDebugSection Debug { get; }
        INodeSection Node { get; }
        ITransactionsSection Transactions { get; }
        IUtilsSection Utils { get; }
        ILeasingSection Leasing { get; }
    }
}
