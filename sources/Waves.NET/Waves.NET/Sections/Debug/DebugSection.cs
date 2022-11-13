using Waves.NET.Debug.ReturnTypes;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Debug
{
    public class DebugSection : SectionBase, IDebugSection
    {
        public DebugSection(HttpClient httpClient) : base(httpClient, "debug") { }

        /// <summary>
        /// Get history of the regular balance at a given address. Max depth is set by <c>waves.db.max-rollback-depth</c>, 2000 by default
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns>History of regular balance</returns>
        public ICollection<HistoryBalance> GetBalanceHistory(string address)
        {
            return PublicRequest<ICollection<HistoryBalance>>(HttpMethod.Get, $"balances/history/{address}");
        }

        //POST /debug/validate
        /// <summary>
        /// Validates a transaction and measures time spent in milliseconds. You should use the JSON transaction format with proofs
        /// </summary>
        /// <typeparam name="TResult">Transaction type</typeparam>
        /// <param name="transaction">Signed transaction</param>
        /// <returns>Validate Transaction</returns>
        public TransactionValidationResult ValidateTransaction<T>(T transaction) where T : Waves.NET.Transactions.Transaction //TODO! not sure
        {
            var jsonBody = JsonUtils.Serialize(transaction);
            return PublicRequest<TransactionValidationResult>(HttpMethod.Post, "validate", jsonBody);
        }
    }
}