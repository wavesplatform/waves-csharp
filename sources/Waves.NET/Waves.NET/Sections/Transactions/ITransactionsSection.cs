using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public interface ITransactionsSection
    {
        T Broadcast<T>(T transaction, bool trace = false) where T : Transaction;
        TransactionFeeAmount CalculateTransactionFee<T>(T transaction) where T : Transaction;
        ICollection<TransactionInfo> GetTransactionInfo(ICollection<string> ids);
        TransactionInfo GetTransactionInfo(string id);
        ICollection<TransactionMerkleProofs> GetTransactionMerkleProofs(ICollection<string> ids);
        ICollection<TransactionInfo> GetTransactionsByAddress(string address, int limit = 1000, string afterTxId = "");
        ICollection<TransactionStatus> GetTransactionsStatus(ICollection<string> ids);
        TransactionStatus GetTransactionStatus(string id);
        ICollection<Transaction> GetUnconfirmedTransaction();
        Transaction GetUnconfirmedTransaction(string id);
        int GetUtxSize();
    }
}