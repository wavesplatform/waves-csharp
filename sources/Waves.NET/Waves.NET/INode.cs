using Waves.NET.Sections;

namespace Waves.NET
{
    public interface INode : IAddressesSection, IAliasesSection, IAssetsSection, IBlockchainSection, IBlocksSection,
        IDebugSection, ILeasingSection, INodeSection, ITransactionsSection, IUtilsSection, IWaitings
    {
        byte ChainId { get; }
    }
}
