using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface IUpdateAssetInfoTransaction : INonGenesisTransaction
    {
        Base58s? AssetId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}