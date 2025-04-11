using WavesLabs.Node.Client.ReturnTypes;
using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions.Utils;

namespace WavesLabs.Node.Client.Sections
{
    public class TransactionsSection : SectionBase, ITransactionsSection
    {
        public TransactionsSection(HttpClient httpClient) : base(httpClient, "transactions") { }

        public TransactionFeeAmount CalculateTransactionFee<T>(T transaction) where T : Transaction
        {
            var jsonBody = JsonUtils.Serialize(transaction);
            return PublicRequest<TransactionFeeAmount>(HttpMethod.Post, "calculateFee", jsonBody);
        }

        public ICollection<TransactionInfo> GetTransactionsByAddress(Address address, int limit = 1000, Base58s? afterTxId = null)
        {
            var url = $"address/{address}/limit/{limit}";

            if (afterTxId != null)
            {
                url += $"?after={afterTxId}";
            }

            // Node returns array of arrays, bug? (o_O)
            var result = PublicRequest<ICollection<ICollection<TransactionInfo>>>(HttpMethod.Get, url, null);
            return result.FirstOrDefault() ?? new List<TransactionInfo>();
        }

        public T Broadcast<T>(T transaction, bool trace = false) where T : Transaction
        {
            var url = $"broadcast" + (trace ? "?trace=true" : "");
            var jsonBody = JsonUtils.Serialize(transaction);
            return PublicRequest<T>(HttpMethod.Post, url, jsonBody);
        }

        public ICollection<TransactionInfo> GetTransactionsInfo(ICollection<Base58s> ids)
        {
            var jsonBody = JsonUtils.Serialize(new { ids });
            return PublicRequest<ICollection<TransactionInfo>>(HttpMethod.Post, "info", jsonBody);
        }

        public ICollection<T> GetTransactionsInfo<T>(ICollection<Base58s> ids) where T : TransactionInfo
        {
            var jsonBody = JsonUtils.Serialize(new { ids });
            return PublicRequest<ICollection<TransactionInfo>>(HttpMethod.Post, "info", jsonBody).Cast<T>().ToList();
        }

        public TransactionInfo GetTransactionInfo(Base58s id)
        {
            return PublicRequest<TransactionInfo>(HttpMethod.Get, $"info/{id}");
        }

        public T GetTransactionInfo<T>(Base58s id) where T : TransactionInfo
        {
            var txi = GetTransactionInfo(id) as T;
            if(txi is null)
            {
                throw new InvalidCastException("GetTransactionInfo: requested transaction info type mismatches");
            }
            return txi;
        }

        public ICollection<TransactionStatus> GetTransactionsStatus(ICollection<Base58s> ids)
        {
            var jsonBody = JsonUtils.Serialize(new { ids });
            return PublicRequest<ICollection<TransactionStatus>>(HttpMethod.Post, "status", jsonBody);
        }

        public TransactionStatus GetTransactionStatus(Base58s id)
        {
            return PublicRequest<TransactionStatus>(HttpMethod.Get, $"status/{id}");
        }

        public ICollection<Transaction> GetUnconfirmedTransactions()
        {
            return PublicRequest<ICollection<Transaction>>(HttpMethod.Get, "unconfirmed");
        }

        public Transaction GetUnconfirmedTransaction(Base58s id)
        {
            return PublicRequest<Transaction>(HttpMethod.Get, $"unconfirmed/info/{id}");
        }

        public int GetUtxSize()
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"unconfirmed/size").size;
        }
    }
}