namespace Waves.NET.Transactions
{
    public class LeaseCancelTransaction : Transaction, ILeaseCancelTransaction
    {
        public const int TYPE = 9;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public string LeaseId { get; set; } = null!;
        public LeaseInfo Lease { get; set; } = null!;
    }

    public interface ILeaseCancelTransaction : INonGenesisTransaction
    {
        string LeaseId { get; set; }
        LeaseInfo Lease { get; set; }
    }
}