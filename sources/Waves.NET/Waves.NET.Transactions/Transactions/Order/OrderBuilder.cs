using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Transactions
{
    public class OrderBuilder
    {
        private Order _order;

        public OrderType OrderType { get; set; }
        public long Amount { get; set; }
        public long Price { get; set; }
        public PublicKey Matcher { get; set; }
        public long Expiration { get; set; }
        public AssetPair AssetPair { get; set; }

        protected int Version { get; private set; }
        protected byte ChainId { get; private set; }
        protected long Fee { get; private set; }
        protected long ExtraFee { get; private set; }
        protected PublicKey SenderPublicKey { get; private set; }
        protected long Timestamp { get; private set; }
        protected ICollection<Base58s> Proofs { get; private set; } = new LinkedList<Base58s>();
        public byte[]? Eip712Signature { get; set; }

        public OrderBuilder(OrderType type, long amount, long price, PublicKey matcher, AssetPair assetPair)
        {
            OrderType = type;
            Amount = amount;
            Price = price;
            Matcher = matcher;
            AssetPair = assetPair;
            ChainId = WavesConfig.ChainId;

            Expiration = 0;
            SenderPublicKey = PublicKey.Zero;
            Version = Order.LatestVersion;
            Fee = Order.MinFee;
            Eip712Signature = null;

            _order = new Order();
        }

        public OrderBuilder SetExpiration(long expiration)
        {
            Expiration = expiration;
            return this;
        }

        public OrderBuilder SetEip712Signature(byte[]? eip712Signature)
        {
            Eip712Signature = eip712Signature;
            return this;
        }

        public static OrderBuilder Params(OrderType type, long amount, long price, PublicKey matcher, AssetPair assetPair)
        {
            return new OrderBuilder(type, amount, price, matcher, assetPair);
        }

        public Order GetSignedWith(PrivateKey signer)
        {
            if (SenderPublicKey is null || SenderPublicKey.IsZero)
            {
                SenderPublicKey = signer.PublicKey;
            }

            var tx = GetUnsigned();
            var bodyBytes = new OrderBinarySerializer().Serialize(_order);
            tx.Id = Base58s.As(Crypto.CalculateBlake2bHash(bodyBytes));
            tx.Proofs.Add(new Base58s(signer.Sign(bodyBytes)));

            return tx;
        }

        public Order GetUnsigned()
        {
            if (Timestamp == 0)
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            _order.Sender = Address.FromPublicKey(ChainId, SenderPublicKey);
            _order.SenderPublicKey = SenderPublicKey;
            _order.Timestamp = Timestamp;
            _order.Proofs = Proofs;
            _order.Version = Version;
            _order.AssetPair = AssetPair;
            _order.MatcherPublicKey = Matcher;
            _order.OrderType = OrderType;
            _order.Amount = Amount;
            _order.Price = Price;
            _order.MatcherFee = Fee;
            _order.Expiration = Expiration == 0 ? Timestamp + (30 * 24 * 60 * 60 * 1000L) : Expiration;
            _order.Eip712Signature = Eip712Signature;
            _order.ChainId = ChainId;

            return _order;
        }

        public OrderBuilder AddProof(Base58s proof)
        {
            if (!string.IsNullOrWhiteSpace(proof))
                Proofs.Add(proof);
            return this;
        }

        public OrderBuilder SetSender(PublicKey sender)
        {
            SenderPublicKey = sender;
            return this;
        }

        public OrderBuilder SetExtraFee(long extraFee)
        {
            ExtraFee = extraFee;
            return this;
        }

        public OrderBuilder SetFee(long fee)
        {
            Fee = fee;
            return this;
        }

        public OrderBuilder SetTimestamp(long timestamp)
        {
            Timestamp = timestamp;
            return this;
        }

        public OrderBuilder SetVersion(int version)
        {
            Version = version;
            return this;
        }
    }
}