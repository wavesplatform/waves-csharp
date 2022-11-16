namespace Waves.NET.Transactions
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

        public static ExchangeTransactionBuilder Params(Order order1, Order order2, long amount, long price, long buyMatcherFee, long sellMatcherFee)
        {
            return new ExchangeTransactionBuilder(order1, order2, amount, price, buyMatcherFee, sellMatcherFee);
        }
    }
}