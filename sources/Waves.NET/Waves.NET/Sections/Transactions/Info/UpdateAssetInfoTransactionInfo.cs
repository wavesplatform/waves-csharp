using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class UpdateAssetInfoTransactionInfo : TransactionInfo
    {
        public UpdateAssetInfoTransactionInfo(UpdateAssetInfoTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override UpdateAssetInfoTransaction Transaction => (UpdateAssetInfoTransaction)base.Transaction;
    }
}