using Waves.NET.Debug.ReturnTypes;
using Waves.NET.Transactions;

namespace Waves.NET.Debug
{
    public interface IDebugSection
    {
        ICollection<HistoryBalance> GetBalanceHistory(string address);
        TransactionValidationResult ValidateTransaction<T>(T transaction) where T : Transaction;
    }
}