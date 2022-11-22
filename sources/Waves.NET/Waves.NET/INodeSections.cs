using Waves.NET.Sections;

namespace Waves.NET
{
    public interface INodeSections : IWaitings
    {
        byte ChainId { get; }

        IAddressesSection Addresses { get; }
        IAliasesSection Aliases { get; }
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
