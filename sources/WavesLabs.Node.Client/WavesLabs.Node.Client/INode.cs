using WavesLabs.Node.Client.Sections;

namespace WavesLabs.Node.Client
{
    public interface INode : IAddressesSection, IAliasesSection, IAssetsSection, IBlockchainSection, IBlocksSection,
        IDebugSection, ILeasingSection, INodeSection, ITransactionsSection, IUtilsSection, IWaitings
    {
        byte ChainId { get; }
    }
}
