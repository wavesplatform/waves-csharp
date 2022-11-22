using Waves.NET.ReturnTypes;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Info;

namespace Waves.NET.Sections
{
    public interface ITransactionsSection
    {
        /// <summary>
        /// Get the minimum fee for a given transaction.<br/>
        /// Transaction data including <c>type</c> and <c>senderPublicKey</c>. To calculate a sponsored fee, specify <c>feeAssetId</c>. <c>fee</c> and <c>sender</c> are ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <returns>Calculated transaction fee</returns>
        public TransactionFeeAmount CalculateTransactionFee<T>(T transaction) where T : Transaction;

        /// <summary>
        /// Get a list of the latest transactions involving a given address. Max number of transactions is set by <c>waves.rest-api.transactions-by-address-limit</c>, 1000 by default.<br/>
        /// For pagination, use the field {after}
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="limit">Number of transactions to be returned</param>
        /// <param name="afterTxId">ID of the transaction to paginate after</param>
        /// <returns>List of transactions by address</returns>
        public ICollection<TransactionInfo> GetTransactionsByAddress(string address, int limit = 1000, string afterTxId = "");

        /// <summary>
        /// Broadcast a signed transaction.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <returns>Broadcast transaction</returns>
        public T Broadcast<T>(T transaction, bool trace = false) where T : Transaction;

        /// <summary>
        /// Get transactions by IDs
        /// </summary>
        /// <param name="ids">Transaction IDs base58 encoded</param>
        /// <returns>Transactions info</returns>
        public ICollection<TransactionInfo> GetTransactionsInfo(ICollection<Base58s> ids);

        /// <summary>
        /// Get transactions by IDs of specified type
        /// </summary>
        /// <param name="ids">Transaction IDs base58 encoded</param>
        /// <returns>Transactions info of specified type</returns>
        public ICollection<T> GetTransactionsInfo<T>(ICollection<Base58s> ids) where T : TransactionInfo;

        /// <summary>
        /// Get transaction by ID
        /// </summary>
        /// <param name="id">Transaction ID base58 encoded</param>
        /// <returns>Transaction info</returns>
        public TransactionInfo GetTransactionInfo(Base58s id);

        /// <summary>
        /// Get transaction by ID of specified type
        /// </summary>
        /// <param name="id">Transaction ID base58 encoded</param>
        /// <returns>Transaction info of specified type</returns>
        public T GetTransactionInfo<T>(Base58s id) where T : TransactionInfo;

        /// <summary>
        /// Get <see href="https://docs.waves.tech/en/blockchain/block/merkle-root#proof-of-transaction-in-block">merkle proofs</see> for given transactions
        /// </summary>
        /// <param name="ids">Transaction IDs</param>
        /// <returns>Merkle proofs</returns>
        public ICollection<TransactionMerkleProofs> GetTransactionMerkleProofs(ICollection<string> ids);

        /// <summary>
        /// Get transaction statuses by their ID. Max number of transactions is set by <c>waves.rest-api.transactions-by-address-limit</c>, 1000 by default.<br/>
        /// Transactions in the response are in the same order as in the request.
        /// </summary>
        /// <param name="ids">Transaction IDs</param>
        /// <returns>Transaction statuses</returns>
        public ICollection<TransactionStatus> GetTransactionsStatus(ICollection<Base58s> ids);

        /// <summary>
        /// Get transaction status by ID
        /// </summary>
        /// <param name="id">Transaction ID base58 encoded</param>
        /// <returns>Transaction status</returns>
        public TransactionStatus GetTransactionStatus(Base58s id);

        /// <summary>
        /// Get a list of transactions in node's UTX pool
        /// </summary>
        /// <returns>Unconfirmed transactions</returns>
        public ICollection<Transaction> GetUnconfirmedTransaction();

        /// <summary>
        /// Get an unconfirmed transaction by its ID
        /// </summary>
        /// <param name="id">Transaction ID base58 encoded</param>
        /// <returns>Unconfirmed transaction info</returns>
        public Transaction GetUnconfirmedTransaction(Base58s id);

        /// <summary>
        /// Get the number of transactions in the UTX pool
        /// </summary>
        /// <returns>Number of unconfirmed transactions</returns>
        public int GetUtxSize();
    }
}