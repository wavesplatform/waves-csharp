using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class ExchangeTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override Dictionary<int, TransactionSchema> VersionToSchemaMap =>
            new Dictionary<int, TransactionSchema> { { 1, TransactionSchema.Signature }, { 2, TransactionSchema.Proofs }, { 3, TransactionSchema.Protobuf } };

        protected override void SerializeToProtobufSchema(TransactionProto proto, Transaction transaction)
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

        protected override void SerializeToProofsSchema(BinaryWriter bw, Transaction transaction)
        {
            throw new NotImplementedException();
        }

        protected override void SerializeToSignatureSchema(BinaryWriter bw, Transaction transaction)
        {
            throw new NotImplementedException();
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
                ChainId = order.ChainId,
                SenderPublicKey = ByteString.CopyFrom(order.SenderPublicKey.Bytes),
                Version = order.Version,
                Timestamp = order.Timestamp
            };
        }
    }
}