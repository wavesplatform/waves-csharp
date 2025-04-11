using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface ISetAssetScriptTransaction : INonGenesisTransaction
    {
        AssetId? AssetId { get; set; }
        string Script { get; set; }
    }
}