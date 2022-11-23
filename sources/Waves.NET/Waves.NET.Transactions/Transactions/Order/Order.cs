using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class Order : IOrder, IEquatable<Order?>

    {
        public const int LatestVersion = 4;
        public const int MinFee = 300000;

        public int Version { get; set; }
        public Base58s? Id { get; set; }
        public IRecipient Sender { get; set; } = null!;
        public PublicKey SenderPublicKey { get; set; } = null!;
        public PublicKey? MatcherPublicKey { get; set; } = null!;
        public AssetPair AssetPair { get; set; } = null!;
        public OrderType OrderType { get; set; }
        public long Amount { get; set; }
        public long Price { get; set; }
        public long Timestamp { get; set; }
        public long Expiration { get; set; }
        public long MatcherFee { get; set; }
        public Base58s? MatcherFeeAssetId { get; set; }
        public ICollection<Base58s> Proofs { get; set; } = new List<Base58s>();
        public byte[]? Eip712Signature { get; set; }
        public byte ChainId { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as Order is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as Order);
        }

        public bool Equals(Order? other)
        {
            return other is not null &&
                   Version == other.Version &&
                   EqualityComparer<Base58s?>.Default.Equals(Id, other.Id) &&
                   EqualityComparer<IRecipient>.Default.Equals(Sender, other.Sender) &&
                   EqualityComparer<PublicKey>.Default.Equals(SenderPublicKey, other.SenderPublicKey) &&
                   EqualityComparer<PublicKey?>.Default.Equals(MatcherPublicKey, other.MatcherPublicKey) &&
                   EqualityComparer<AssetPair>.Default.Equals(AssetPair, other.AssetPair) &&
                   OrderType == other.OrderType &&
                   Amount == other.Amount &&
                   Price == other.Price &&
                   Timestamp == other.Timestamp &&
                   Expiration == other.Expiration &&
                   MatcherFee == other.MatcherFee &&
                   EqualityComparer<Base58s?>.Default.Equals(MatcherFeeAssetId, other.MatcherFeeAssetId) &&
                   (Proofs is null && other.Proofs is null || Proofs.SequenceEqual(other.Proofs)) &&
                   (Eip712Signature is null && other.Eip712Signature is null || Eip712Signature.SequenceEqual(other.Eip712Signature));
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Version);
            hash.Add(Id);
            hash.Add(Sender);
            hash.Add(SenderPublicKey);
            hash.Add(MatcherPublicKey);
            hash.Add(AssetPair);
            hash.Add(OrderType);
            hash.Add(Amount);
            hash.Add(Price);
            hash.Add(Timestamp);
            hash.Add(Expiration);
            hash.Add(MatcherFee);
            hash.Add(MatcherFeeAssetId);
            hash.Add(Proofs.CalcHashCode());
            hash.Add(Eip712Signature?.CalcHashCode());
            return hash.ToHashCode();
        }

        public static bool operator ==(Order? left, Order? right) => EqualityComparer<Order>.Default.Equals(left, right);
        public static bool operator !=(Order? left, Order? right) => !(left == right);
    }
}