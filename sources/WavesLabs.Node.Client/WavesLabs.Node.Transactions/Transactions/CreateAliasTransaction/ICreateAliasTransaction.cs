namespace WavesLabs.Node.Transactions
{
    public interface ICreateAliasTransaction : INonGenesisTransaction
    {
        string Alias { get; set; }
    }
}