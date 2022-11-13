namespace Waves.NET.Transactions
{
    public class GenesisTransaction : Transaction, IGenesisTransaction
    {
        public const int TYPE = 1;
        public const int LatestVersion = 1;

        public string Recipient { get; set; } = null!;
        public long Amount { get; set; }
    }

    public interface IGenesisTransaction : ITransaction
    {
        string Recipient { get; set; }
        long Amount { get; set; }
    }
}