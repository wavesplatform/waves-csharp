using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public record Transfer
    {
        public IRecipient Recipient { get; init; } = null!;
        public long Amount { get; init; }
    }
}