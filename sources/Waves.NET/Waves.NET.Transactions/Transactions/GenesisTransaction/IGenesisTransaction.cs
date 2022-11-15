using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface IGenesisTransaction : ITransaction
    {
        IRecipient Recipient { get; set; }
        long Amount { get; set; }
    }
}