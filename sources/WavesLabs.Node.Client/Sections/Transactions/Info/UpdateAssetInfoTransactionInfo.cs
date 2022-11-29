using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class UpdateAssetInfoTransactionInfo : TransactionInfo
    {
        public UpdateAssetInfoTransactionInfo(UpdateAssetInfoTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override UpdateAssetInfoTransaction Transaction => (UpdateAssetInfoTransaction)base.Transaction;
    }
}