using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public record EthTransactionTransferPayload : EthTransactionPayload
    {
        public Address Recipient { get; init; } = null!;
        public Base58s Asset { get; init; } = null!;
        public long Amount { get; init; }
    }
}