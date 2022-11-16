using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class GenesisTransactionInfo : TransactionInfo
    {
        public GenesisTransactionInfo(GenesisTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override GenesisTransaction Transaction => (GenesisTransaction)base.Transaction;
    }
}