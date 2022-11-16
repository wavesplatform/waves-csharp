namespace Waves.NET.Transactions
{
    public interface ICreateAliasTransaction : INonGenesisTransaction
    {
        string Alias { get; set; }
    }
}