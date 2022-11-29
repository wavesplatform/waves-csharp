using WavesLabs.Node.Client.ReturnTypes;
using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Utils;

namespace WavesLabs.Node.Client.Sections
{
    public class DebugSection : SectionBase, IDebugSection
    {
        public DebugSection(HttpClient httpClient) : base(httpClient, "debug") { }

        public ICollection<HistoryBalance> GetBalanceHistory(string address)
        {
            return PublicRequest<ICollection<HistoryBalance>>(HttpMethod.Get, $"balances/history/{address}");
        }

        public TransactionValidationResult ValidateTransaction<T>(T transaction) where T : Transaction
        {
            var jsonBody = JsonUtils.Serialize(transaction);
            return PublicRequest<TransactionValidationResult>(HttpMethod.Post, "validate", jsonBody);
        }
    }
}