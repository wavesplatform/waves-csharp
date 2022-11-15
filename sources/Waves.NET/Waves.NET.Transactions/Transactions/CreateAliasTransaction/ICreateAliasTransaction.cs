using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions
{
    public interface ICreateAliasTransaction : INonGenesisTransaction
    {
        Alias Alias { get; set; }
    }
}