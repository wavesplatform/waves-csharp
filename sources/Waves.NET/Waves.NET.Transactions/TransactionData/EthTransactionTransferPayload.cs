using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public record EthTransactionTransferPayload : EthTransactionPayload
    {
        public Address Recipient { get; init; } = null!;
        public Base58s Asset { get; init; } = null!;
        public long Amount { get; init; }
    }
}