using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public record EthTransactionInvokePayload : EthTransactionPayload
    {
        public IRecipient DApp { get; set; } = null!;
        public ICollection<Amount> Payment { get; set; } = new List<Amount>();
        public Call Call { get; set; } = null!;
        public StateChanges StateChanges { get; set; } = null!;
    }
}