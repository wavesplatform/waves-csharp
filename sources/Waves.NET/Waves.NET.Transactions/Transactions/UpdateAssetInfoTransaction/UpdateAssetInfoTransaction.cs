using Waves.NET.Transactions.Common;
using Waves.NET.Transactions;

namespace Waves.NET.Transactions
{
    public class UpdateAssetInfoTransaction : Transaction, IUpdateAssetInfoTransaction
    {
        public const int TYPE = 17;
        public const int LatestVersion = 1;
        public const int MinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}