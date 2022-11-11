using Waves.NET.Transactions.Info;

namespace Waves.NET.Transactions
{
    public class TransactionsSection : SectionBase, ITransactionsSection
    {
        public TransactionsSection(HttpClient httpClient, byte chainId) : base(httpClient, "transactions", chainId) { }

        /// <summary>
        /// Get the minimum fee for a given transaction.<br/>
        /// Transaction data including <c>type</c> and <c>senderPublicKey</c>. To calculate a sponsored fee, specify <c>feeAssetId</c>. <c>fee</c> and <c>sender</c> are ignored.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <returns>Calculated transaction fee</returns>
        public TransactionFeeAmount CalculateTransactionFee<T>(T transaction) where T : Transaction
        {
            var jsonBody = SerializeObject(transaction);
            return PublicRequest<TransactionFeeAmount>(HttpMethod.Post, "calculateFee", jsonBody);
        }

        /// <summary>
        /// Get a list of the latest transactions involving a given address. Max number of transactions is set by <c>waves.rest-api.transactions-by-address-limit</c>, 1000 by default.<br/>
        /// For pagination, use the field {after}
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="limit">Number of transactions to be returned</param>
        /// <param name="afterTxId">ID of the transaction to paginate after</param>
        /// <returns>List of transactions by address</returns>
        public ICollection<TransactionInfo> GetTransactionsByAddress(string address, int limit = 1000, string afterTxId = "")
        {
            var url = $"address/{address}/limit/{limit}";

            if (!string.IsNullOrWhiteSpace(afterTxId))
            {
                url += $"?after={afterTxId}";
            }

            return PublicRequest<ICollection<TransactionInfo>>(HttpMethod.Get, url, null); //TODO! json converter?
        }

        /// <summary>
        /// Broadcast a signed transaction.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transaction"></param>
        /// <returns>Broadcast transaction</returns>
        public T Broadcast<T>(T transaction, bool trace = false) where T : Transaction
        {
            var url = $"broadcast" + (trace ? "?trace=true" : "");
            var jsonBody = SerializeObject(transaction);
            return PublicRequest<T>(HttpMethod.Post, url, jsonBody);
        }

        /// <summary>
        /// Get transactions by IDs
        /// </summary>
        /// <param name="ids">Transaction IDs base58 encoded</param>
        /// <returns>Transactions info</returns>
        public ICollection<TransactionInfo> GetTransactionInfo(ICollection<string> ids)
        {
            var jsonBody = SerializeObject(new { ids });
            return PublicRequest<ICollection<TransactionInfo>>(HttpMethod.Post, "info", jsonBody);
        }

        /// <summary>
        /// Get transactions by ID
        /// </summary>
        /// <param name="id">Transaction ID base58 encoded</param>
        /// <returns>Transaction info</returns>
        public TransactionInfo GetTransactionInfo(string id)
        {
            return PublicRequest<TransactionInfo>(HttpMethod.Get, $"info/{id}");
        }
        /// <summary>
        /// Get <see href="https://docs.waves.tech/en/blockchain/block/merkle-root#proof-of-transaction-in-block">merkle proofs</see> for given transactions
        /// </summary>
        /// <param name="ids">Transaction IDs</param>
        /// <returns>Merkle proofs</returns>
        public ICollection<TransactionMerkleProofs> GetTransactionMerkleProofs(ICollection<string> ids)
        {
            var jsonBody = SerializeObject(new { ids });
            return PublicRequest<ICollection<TransactionMerkleProofs>>(HttpMethod.Post, "merkleProof", jsonBody);
        }

        //POST /transactions/sign
        //Sign transaction on behalf of wallet account

        //POST /transactions/sign/{signerAddress}
        //Sign transaction on behalf of wallet account

        /// <summary>
        /// Get transaction statuses by their ID. Max number of transactions is set by <c>waves.rest-api.transactions-by-address-limit</c>, 1000 by default.<br/>
        /// Transactions in the response are in the same order as in the request.
        /// </summary>
        /// <param name="ids">Transaction IDs</param>
        /// <returns>Transaction statuses</returns>
        public ICollection<TransactionStatus> GetTransactionsStatus(ICollection<string> ids)
        {
            var jsonBody = SerializeObject(new { ids });
            return PublicRequest<ICollection<TransactionStatus>>(HttpMethod.Post, "status", jsonBody);
        }

        /// <summary>
        /// Get transaction status by ID
        /// </summary>
        /// <param name="id">Transaction ID base58 encoded</param>
        /// <returns>Transaction status</returns>
        public TransactionStatus GetTransactionStatus(string id)
        {
            return PublicRequest<TransactionStatus>(HttpMethod.Get, $"status/{id}");
        }

        /// <summary>
        /// Get a list of transactions in node's UTX pool
        /// </summary>
        /// <returns>Unconfirmed transactions</returns>
        public ICollection<Transaction> GetUnconfirmedTransaction()
        {
            return PublicRequest<ICollection<Transaction>>(HttpMethod.Get, "unconfirmed");
        }

        /// <summary>
        /// Get an unconfirmed transaction by its ID
        /// </summary>
        /// <param name="id">Transaction ID base58 encoded</param>
        /// <returns>Unconfirmed transaction info</returns>
        public Transaction GetUnconfirmedTransaction(string id)
        {
            return PublicRequest<Transaction>(HttpMethod.Get, $"unconfirmed/info/{id}");
        }

        /// <summary>
        /// Get the number of transactions in the UTX pool
        /// </summary>
        /// <returns>Number of unconfirmed transactions</returns>
        public int GetUtxSize()
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"unconfirmed/size").size;
        }
    }
}