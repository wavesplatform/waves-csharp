using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface ISetAssetScriptTransaction : INonGenesisTransaction
    {
        Base58s? AssetId { get; set; }
        string Script { get; set; }
    }
}