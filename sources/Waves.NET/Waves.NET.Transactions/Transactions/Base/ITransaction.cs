using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public interface ITransaction
    {
        int Type { get; set; }
        Base58s? Id { get; set; }
        long Timestamp { get; set; }
        long Fee { get; set; }
        Base58s? Signature { get; set; }

        int Height { get; set; }
        byte ChainId { get; set; }
    }
}
