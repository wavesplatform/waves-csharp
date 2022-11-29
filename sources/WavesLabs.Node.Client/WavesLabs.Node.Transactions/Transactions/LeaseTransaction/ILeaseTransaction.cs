using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface ILeaseTransaction : INonGenesisTransaction
    {
        IRecipient Recipient { get; set; }
        long Amount { get; set; }
    }
}