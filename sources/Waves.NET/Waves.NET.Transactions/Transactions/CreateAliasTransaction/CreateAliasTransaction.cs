using Waves.NET.Transactions.Crypto;
using Waves.NET.Transactions;

namespace Waves.NET.Transactions
{
    public class CreateAliasTransaction : Transaction, ICreateAliasTransaction
    {
        public const int TYPE = 10;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public Alias Alias { get; set; } = null!;
    }
}