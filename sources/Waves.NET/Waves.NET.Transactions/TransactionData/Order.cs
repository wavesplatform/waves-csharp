using Waves.NET.Transactions.Crypto;
using Waves.NET.Transactions;

namespace Waves.NET.Transactions
{
    public class Order : Transaction, IOrder
    {
        public PublicKey MatcherPublicKey { get; set; } = null!;
        public AssetPair AssetPair { get; set; } = new AssetPair();
        public OrderType OrderType { get; set; }
        public long Amount { get; set; }
        public long Price { get; set; }
        public long Expiration { get; set; }
        public long MatcherFee { get; set; }
        public string MatcherFeeAssetId { get; set; } = "";
    }
}