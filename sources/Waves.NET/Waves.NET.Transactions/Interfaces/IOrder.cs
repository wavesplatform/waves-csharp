using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface IOrder : INonGenesisTransaction
    {
        long Amount { get; set; }
        AssetPair AssetPair { get; set; }
        long Expiration { get; set; }
        long MatcherFee { get; set; }
        string MatcherFeeAssetId { get; set; }
        PublicKey MatcherPublicKey { get; set; }
        OrderType OrderType { get; set; }
        long Price { get; set; }
    }
}
