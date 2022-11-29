using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface IBurnTransaction
    {
        Base58s? AssetId { get; set; }
        long Amount { get; set; }
    }
}