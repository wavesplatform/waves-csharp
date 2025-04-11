using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface ICreateAliasTransaction : INonGenesisTransaction
    {
        Alias Alias { get; set; }
    }
}