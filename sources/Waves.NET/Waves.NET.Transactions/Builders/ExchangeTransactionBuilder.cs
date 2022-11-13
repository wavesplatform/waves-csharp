using Google.Protobuf;

namespace Waves.NET.Transactions.Builders
{
    public class ExchangeTransactionBuilder : TransactionBuilder<ExchangeTransactionBuilder, ExchangeTransaction> //TODO! check actions over order
    {
        public ExchangeTransactionBuilder() : base(ExchangeTransaction.LatestVersion, ExchangeTransaction.MinFee, ExchangeTransaction.TYPE) { }

        public ExchangeTransactionBuilder(Order order1, Order order2, long amount, long price, long buyMatcherFee, long sellMatcherFee) : this()
        {
            Transaction.Order1 = order1;
            Transaction.Order2 = order2;
            Transaction.Amount = amount;
            Transaction.Price = price;
            Transaction.BuyMatcherFee = buyMatcherFee;
            Transaction.SellMatcherFee = sellMatcherFee;
        }

        public static ExchangeTransactionBuilder Data(Order order1, Order order2, long amount, long price, long buyMatcherFee, long sellMatcherFee)
        {
            return new ExchangeTransactionBuilder(order1, order2, amount, price, buyMatcherFee, sellMatcherFee);
        }

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (IExchangeTransaction)Transaction;
            var p = new ExchangeTransactionData();

            p.Orders.Add(OrderToProtobuf(tx.Order1));
            p.Orders.Add(OrderToProtobuf(tx.Order2));
            p.Amount = tx.Amount;
            p.Price = tx.Price;
            p.BuyMatcherFee = tx.BuyMatcherFee;
            p.SellMatcherFee = tx.SellMatcherFee;

            proto.Exchange = p;
        }

        private Waves.Order OrderToProtobuf(Order order)
        {
            return new Waves.Order
            {
                MatcherPublicKey = ByteString.CopyFrom(order.MatcherPublicKey.Bytes),
                AssetPair = new Waves.AssetPair
                {
                    AmountAssetId = ByteString.CopyFromUtf8(order.AssetPair.AmountAsset),
                    PriceAssetId = ByteString.CopyFromUtf8(order.AssetPair.PriceAsset)
                },
                OrderSide = order.OrderType == OrderType.Buy ? Waves.Order.Types.Side.Buy : Waves.Order.Types.Side.Sell,
                Amount = order.Amount,
                Price = order.Price,
                Expiration = order.Expiration,
                MatcherFee = new AmountProto
                {
                    Amount_ = order.MatcherFee,
                    AssetId = ByteString.CopyFromUtf8(order.MatcherFeeAssetId)
                },
                ChainId = ChainId,
                SenderPublicKey = ByteString.CopyFrom(SenderPublicKey.Bytes),
                Version = Version,
                Timestamp = Timestamp
            };
        }
    }
}