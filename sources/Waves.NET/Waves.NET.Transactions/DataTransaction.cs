namespace Waves.NET.Transactions
{
    public class DataTransaction : Transaction, IDataTransaction
    {
        public const int TYPE = 12;
        public const int LatestVersion = 2;
        public const int MinFee = 100000;

        public ICollection<EntryData> Data { get; set; } = null!;
    }

    public interface IDataTransaction : INonGenesisTransaction
    {
        ICollection<EntryData> Data { get; set; }
    }
}