using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface ILeaseCancelTransaction : INonGenesisTransaction
    {
        Base58s LeaseId { get; set; }
        LeaseInfo? Lease { get; set; }
    }
}