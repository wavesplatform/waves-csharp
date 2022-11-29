using WavesLabs.Node.Client.Sections;

namespace WavesLabs.Node.Client
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
        INodeSection Node_ { get; }
        ITransactionsSection Transactions { get; }
        IUtilsSection Utils { get; }
        ILeasingSection Leasing { get; }
    }
}
