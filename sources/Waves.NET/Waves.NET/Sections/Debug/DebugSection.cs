﻿using Waves.NET.ReturnTypes;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Sections
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