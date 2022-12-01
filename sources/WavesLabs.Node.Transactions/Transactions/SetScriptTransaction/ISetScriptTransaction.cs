using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface ISetScriptTransaction : INonGenesisTransaction
    {
        Base64s Script { get; set; }
    }
}