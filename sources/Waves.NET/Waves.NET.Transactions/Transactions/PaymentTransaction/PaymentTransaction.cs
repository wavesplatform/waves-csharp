using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class PaymentTransaction : Transaction, IPaymentTransaction
    {
        public const int TYPE = 2;
        public const int LatestVersion = 1;
        public const int MinFee = 1;

        public Address Recipient { get; set; } = null!;
        public long Amount { get; set; }
    }
}