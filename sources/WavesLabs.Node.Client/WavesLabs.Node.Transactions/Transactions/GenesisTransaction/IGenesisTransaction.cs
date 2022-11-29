using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface IGenesisTransaction : ITransaction
    {
        Address Recipient { get; set; }
        long Amount { get; set; }
    }
}