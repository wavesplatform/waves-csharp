namespace WavesLabs.Node.Transactions
{
    public interface ISetScriptTransaction : INonGenesisTransaction
    {
        string Script { get; set; }
    }
}