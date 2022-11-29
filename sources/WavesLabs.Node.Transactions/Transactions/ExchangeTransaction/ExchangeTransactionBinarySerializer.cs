using Waves;

namespace WavesLabs.Node.Transactions
{
    public class ExchangeTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 3 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IExchangeTransaction)transaction;

            proto.Exchange = new ExchangeTransactionData();
            proto.Exchange.Amount = tx.Amount;
            proto.Exchange.Price = tx.Price;
            proto.Exchange.BuyMatcherFee = tx.BuyMatcherFee;
            proto.Exchange.SellMatcherFee = tx.SellMatcherFee;
            proto.Exchange.Orders.Add(OrderBinarySerializer.OrderToProtobuf(tx.Order1));
            proto.Exchange.Orders.Add(OrderBinarySerializer.OrderToProtobuf(tx.Order2));
        }
    }
}