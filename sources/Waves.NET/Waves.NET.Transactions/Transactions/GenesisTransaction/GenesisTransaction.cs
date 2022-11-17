using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class GenesisTransaction : Transaction, IGenesisTransaction
    {
        public const int TYPE = 1;
        public const int LatestVersion = 1;

        public Address Recipient { get; set; } = null!;
        public long Amount { get; set; }
    }
}