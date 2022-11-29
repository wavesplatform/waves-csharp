using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class SponsorFeeTransactionInfo : TransactionInfo
    {
        public SponsorFeeTransactionInfo(SponsorFeeTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) {}

        public override SponsorFeeTransaction Transaction => (SponsorFeeTransaction)base.Transaction;
    }
}