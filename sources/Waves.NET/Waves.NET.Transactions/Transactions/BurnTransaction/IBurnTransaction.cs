using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface IBurnTransaction
    {
        Base58s? AssetId { get; set; }
        long Amount { get; set; }
    }
}