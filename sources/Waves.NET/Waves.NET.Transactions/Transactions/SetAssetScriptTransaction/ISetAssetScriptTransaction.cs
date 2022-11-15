using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface ISetAssetScriptTransaction : INonGenesisTransaction
    {
        Base58s? AssetId { get; set; }
        string Script { get; set; }
    }
}