using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class SponsorFeeTransactionInfo : TransactionInfo
    {
        public SponsorFeeTransactionInfo(SponsorFeeTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override SponsorFeeTransaction Transaction => (SponsorFeeTransaction)base.Transaction;
    }
}