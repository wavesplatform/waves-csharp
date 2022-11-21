using Waves.NET.Debug;
using Waves.NET.Transactions;

namespace Waves.NET.Debug
{
    public interface IDebugSection
    {
        /// <summary>
        /// Get history of the regular balance at a given address. Max depth is set by <c>waves.db.max-rollback-depth</c>, 2000 by default
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns>History of regular balance</returns>
        ICollection<HistoryBalance> GetBalanceHistory(string address);

        //POST /debug/validate
        /// <summary>
        /// Validates a transaction and measures time spent in milliseconds. You should use the JSON transaction format with proofs
        /// </summary>
        /// <typeparam name="TResult">Transaction type</typeparam>
        /// <param name="transaction">Signed transaction</param>
        /// <returns>Validate Transaction</returns>
        TransactionValidationResult ValidateTransaction<T>(T transaction) where T : Transaction;
    }
}