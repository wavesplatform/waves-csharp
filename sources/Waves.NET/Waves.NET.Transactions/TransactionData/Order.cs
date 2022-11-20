using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class Order : Transaction, IOrder, IEquatable<Order?>
    {
        public PublicKey MatcherPublicKey { get; set; } = null!;
        public AssetPair AssetPair { get; set; } = new AssetPair();
        public OrderType OrderType { get; set; }
        public long Amount { get; set; }
        public long Price { get; set; }
        public long Expiration { get; set; }
        public long MatcherFee { get; set; }
        public string MatcherFeeAssetId { get; set; } = "";

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as Order is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as Order);
        }

        public bool Equals(Order? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<PublicKey>.Default.Equals(MatcherPublicKey, other.MatcherPublicKey) &&
                   EqualityComparer<AssetPair>.Default.Equals(AssetPair, other.AssetPair) &&
                   OrderType == other.OrderType &&
                   Amount == other.Amount &&
                   Price == other.Price &&
                   Expiration == other.Expiration &&
                   MatcherFee == other.MatcherFee &&
                   MatcherFeeAssetId == other.MatcherFeeAssetId;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(MatcherPublicKey);
            hash.Add(AssetPair);
            hash.Add(OrderType);
            hash.Add(Amount);
            hash.Add(Price);
            hash.Add(Expiration);
            hash.Add(MatcherFee);
            hash.Add(MatcherFeeAssetId);
            return hash.ToHashCode();
        }

        public static bool operator ==(Order? left, Order? right) => EqualityComparer<Order>.Default.Equals(left, right);
        public static bool operator !=(Order? left, Order? right) => !(left == right);
    }
}