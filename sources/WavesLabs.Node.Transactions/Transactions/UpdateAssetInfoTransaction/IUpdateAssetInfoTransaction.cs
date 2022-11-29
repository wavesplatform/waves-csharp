using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface IUpdateAssetInfoTransaction : INonGenesisTransaction
    {
        Base58s? AssetId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}