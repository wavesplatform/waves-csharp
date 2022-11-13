using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions
{
    public class InvokeScriptTransaction : Transaction, IInvokeScriptTransaction
    {
        public const int TYPE = 16;
        public const int LatestVersion = 2;
        public const int MinFee = 500000;

        public IRecipient DApp { get; set; } = null!;
        public ICollection<Payment> Payment { get; set; } = null!;
        public Call Call { get; set; } = null!;
        public StateChanges StateChanges { get; set; } = null!;
    }

    public interface IInvokeScriptTransaction : INonGenesisTransaction
    {
        IRecipient DApp { get; set; }
        ICollection<Payment> Payment { get; set; }
        Call Call { get; set; }
        StateChanges StateChanges { get; set; }
    }
}