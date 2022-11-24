using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public record EthTransactionInvokePayload : EthTransactionPayload
    {
        public IRecipient DApp { get; set; } = null!;
        public ICollection<Amount> Payment { get; set; } = new List<Amount>();
        public Call Call { get; set; } = null!;
        public StateChanges StateChanges { get; set; } = null!;
    }
}