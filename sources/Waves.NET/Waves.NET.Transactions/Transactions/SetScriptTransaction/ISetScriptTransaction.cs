namespace Waves.NET.Transactions
{
    public interface ISetScriptTransaction : INonGenesisTransaction
    {
        string Script { get; set; }
    }
}