namespace WavesLabs.Node.Transactions
{
    public class ExchangeTransaction : Transaction, IExchangeTransaction, IEquatable<ExchangeTransaction?>
    {
        public const int TYPE = 7;
        public const int LatestVersion = 3;
        public const int MinFee = 300000;

        public Order Order1 { get; set; } = null!;
        public Order Order2 { get; set; } = null!;
        public long Amount { get; set; }
        public long Price { get; set; }
        public long BuyMatcherFee { get; set; }
        public long SellMatcherFee { get; set; }

        public ExchangeTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as ExchangeTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as ExchangeTransaction);
        }

        public bool Equals(ExchangeTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<Order>.Default.Equals(Order1, other.Order1) &&
                   EqualityComparer<Order>.Default.Equals(Order2, other.Order2) &&
                   Amount == other.Amount &&
                   Price == other.Price &&
                   BuyMatcherFee == other.BuyMatcherFee &&
                   SellMatcherFee == other.SellMatcherFee;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Order1, Order2, Amount, Price, BuyMatcherFee, SellMatcherFee);
        public static bool operator ==(ExchangeTransaction? left, ExchangeTransaction? right) => EqualityComparer<ExchangeTransaction>.Default.Equals(left, right);
        public static bool operator !=(ExchangeTransaction? left, ExchangeTransaction? right) => !(left == right);
    }
}