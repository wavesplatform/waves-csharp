namespace WavesLabs.Node.Transactions
{
    public interface IDataTransaction : INonGenesisTransaction
    {
        ICollection<EntryData> Data { get; set; }
    }
}