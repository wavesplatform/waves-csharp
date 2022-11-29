using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Transactions
{
    public class CreateAliasTransactionInfo : TransactionInfo
    {
        public CreateAliasTransactionInfo(CreateAliasTransaction transaction, ApplicationStatus? applicationStatus, int height) : base(transaction, applicationStatus, height) { }

        public override CreateAliasTransaction Transaction => (CreateAliasTransaction)base.Transaction;
    }
}