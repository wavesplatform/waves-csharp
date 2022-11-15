namespace Waves.NET.Transactions
{
    public interface IDataTransaction : INonGenesisTransaction
    {
        ICollection<EntryData> Data { get; set; }
    }
}