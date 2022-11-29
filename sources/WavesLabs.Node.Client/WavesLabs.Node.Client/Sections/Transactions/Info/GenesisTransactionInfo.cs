using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class GenesisTransactionInfo : TransactionInfo
    {
        public GenesisTransactionInfo(GenesisTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override GenesisTransaction Transaction => (GenesisTransaction)base.Transaction;
    }
}