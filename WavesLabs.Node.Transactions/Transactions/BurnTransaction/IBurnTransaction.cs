using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface IBurnTransaction
    {
        AssetId? AssetId { get; set; }
        long Amount { get; set; }
    }
}