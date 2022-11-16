using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface IPaymentTransaction : INonGenesisTransaction
    {
        Address Recipient { get; set; }
        long Amount { get; set; }
    }
}