using Waves.NET.Transactions.Common;
using Waves.NET.Transactions;

namespace Waves.NET.Transactions
{
    public class SetAssetScriptTransaction : Transaction, ISetAssetScriptTransaction
    {
        public const int TYPE = 15;
        public const int LatestVersion = 2;
        public const int MinFee = 100000000;

        public Base58s? AssetId { get; set; } = null!;
        public string Script { get; set; } = null!;
    }
}