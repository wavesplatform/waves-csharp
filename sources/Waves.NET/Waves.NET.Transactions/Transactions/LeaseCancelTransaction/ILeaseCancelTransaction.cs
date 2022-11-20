using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface ILeaseCancelTransaction : INonGenesisTransaction
    {
        Base58s LeaseId { get; set; }
        LeaseInfo? Lease { get; set; }
    }
}