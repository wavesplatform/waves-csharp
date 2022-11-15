using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface IIssueTransaction : INonGenesisTransaction
    {
        Base58s? AssetId { get; set; }
        string Name { get; set; }
        long Amount { get; set; }
        long Quantity { get; set; }
        bool Reissuable { get; set; }
        int Decimals { get; set; }
        string Description { get; set; }
        string Script { get; set; }
    }
}