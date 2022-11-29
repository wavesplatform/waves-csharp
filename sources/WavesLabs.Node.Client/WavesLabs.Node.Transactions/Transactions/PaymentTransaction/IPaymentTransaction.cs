using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface IPaymentTransaction : INonGenesisTransaction
    {
        Address Recipient { get; set; }
        long Amount { get; set; }
    }
}