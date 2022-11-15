using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface ILeaseTransaction : INonGenesisTransaction
    {
        IRecipient Recipient { get; set; }
        long Amount { get; set; }
    }
}