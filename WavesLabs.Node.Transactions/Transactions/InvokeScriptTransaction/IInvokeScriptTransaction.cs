using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface IInvokeScriptTransaction : INonGenesisTransaction
    {
        IRecipient DApp { get; set; }
        ICollection<Amount> Payment { get; set; }
        Call Call { get; set; }
        StateChanges StateChanges { get; set; }
    }
}