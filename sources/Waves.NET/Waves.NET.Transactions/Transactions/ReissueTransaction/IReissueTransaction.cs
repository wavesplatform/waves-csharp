using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface IReissueTransaction : INonGenesisTransaction
    {
        Base58s? AssetId { get; set; }
        long Amount { get; set; }
        long Quantity { get; set; }
        bool Reissuable { get; set; }
    }
}