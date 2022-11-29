using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
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
        Base64s? Script { get; set; }
    }
}