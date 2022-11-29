using Google.Protobuf;
using Waves;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class OrderBinarySerializer
    {
        protected IList<int> SupportedVersions => new List<int> { 4 };

        public byte[] Serialize(Order order)
        {
            if (SupportedVersions.Any(x => x == order.Version))
            {
                return OrderToProtobuf(order).ToByteArray();
            }

            throw new NotSupportedException($"Ver.{order.Version} of an order is not supported.");
        }

        public static OrderProto OrderToProtobuf(Order order)
        {
            var proto = new OrderProto();

            proto.OrderSide = order.OrderType == OrderType.Buy ? OrderProto.Types.Side.Buy : OrderProto.Types.Side.Sell;
            proto.Version = order.Version;
            proto.ChainId = order.ChainId;
            proto.AssetPair = new AssetPairProto
            {
                AmountAssetId = order.AssetPair.AmountAsset is null ? ByteString.Empty : ByteString.CopyFrom(order.AssetPair.AmountAsset),
                PriceAssetId = order.AssetPair.PriceAsset is null ? ByteString.Empty : ByteString.CopyFrom(order.AssetPair.PriceAsset)
            };
            proto.Amount = order.Amount;
            proto.Price = order.Price;
            proto.MatcherPublicKey = order.MatcherPublicKey is null ? ByteString.Empty : ByteString.CopyFrom(order.MatcherPublicKey);
            proto.MatcherFee = new AmountProto
            {
                Amount_ = order.MatcherFee,
                AssetId = order.MatcherFeeAssetId is null ? ByteString.Empty : ByteString.CopyFrom(order.MatcherFeeAssetId)
            };
            proto.Timestamp = order.Timestamp;
            proto.Expiration = order.Expiration;

            foreach (var p in order.Proofs ?? new List<Base58s>())
            {
                proto.Proofs.Add(ByteString.CopyFrom(p));
            }

            if(order.SenderPublicKey != null)
            {
                proto.SenderPublicKey = ByteString.CopyFrom(order.SenderPublicKey);
            }
            else if(order.Eip712Signature != null)
            {
                proto.Eip712Signature = ByteString.CopyFrom(order.Eip712Signature);
            }

            return proto;
        }
    }
}
