using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class ExchangeTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 3 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IExchangeTransaction)transaction;
            var p = new ExchangeTransactionData();

            p.Orders.Add(OrderToProtobuf(tx.Order1));
            p.Orders.Add(OrderToProtobuf(tx.Order2));
            p.Amount = tx.Amount;
            p.Price = tx.Price;
            p.BuyMatcherFee = tx.BuyMatcherFee;
            p.SellMatcherFee = tx.SellMatcherFee;

            proto.Exchange = p;
        }

        private Waves.OrderProto OrderToProtobuf(Order order)
        {
            return new Waves.OrderProto
            {
                MatcherPublicKey = ByteString.CopyFrom(order.MatcherPublicKey.Bytes),
                AssetPair = new Waves.AssetPair
                {
                    AmountAssetId = ByteString.CopyFromUtf8(order.AssetPair.AmountAsset),
                    PriceAssetId = ByteString.CopyFromUtf8(order.AssetPair.PriceAsset)
                },
                OrderSide = order.OrderType == OrderType.Buy ? Waves.OrderProto.Types.Side.Buy : Waves.OrderProto.Types.Side.Sell,
                Amount = order.Amount,
                Price = order.Price,
                Expiration = order.Expiration,
                MatcherFee = new AmountProto
                {
                    Amount_ = order.MatcherFee,
                    AssetId = ByteString.CopyFromUtf8(order.MatcherFeeAssetId)
                },
                ChainId = order.ChainId,
                SenderPublicKey = ByteString.CopyFrom(order.SenderPublicKey.Bytes),
                Version = order.Version,
                Timestamp = order.Timestamp
            };
        }
    }
}