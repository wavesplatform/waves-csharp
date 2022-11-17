using System.Text.RegularExpressions;
using Waves.NET.Addresses.ReturnTypes;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Addresses
{
    public class AddressesSection : SectionBase, IAddressesSection
    {
        private const string BalanceUrl = "balance";
        private const string DataUrl = "data";
        private const string EffectiveBalanceUrl = "effectiveBalance";
        private const string ScriptInfoUrl = "scriptInfo";

        public AddressesSection(HttpClient httpClient) : base(httpClient, "addresses") { }

        public ICollection<Address> GetAddresses()
        {
            return PublicRequest<ICollection<Address>>(HttpMethod.Get);
        }

        public ICollection<Address> GetAddresses(int from, int to)
        {
            return PublicRequest<ICollection<Address>>(HttpMethod.Get, $"seq/{from}/{to}");
        }

        public ICollection<AddressBalance> GetBalances(ICollection<Address> addresses, int height, string asset)
        {
            return PublicRequest<ICollection<AddressBalance>>(HttpMethod.Post, BalanceUrl, JsonUtils.Serialize(new { addresses, height, asset }));
        }

        public long GetBalance(Address address)
        {
            var result = PublicRequest<AddressBalance>(HttpMethod.Get, BalanceUrl + "/" + address);
            return result.Balance;
        }

        public long GetBalance(Address address, int confirmations)
        {
            var result = PublicRequest<AddressBalance>(HttpMethod.Get, $"{BalanceUrl}/{address}/{confirmations}");
            return result.Balance;

        }

        public BalanceDetails GetBalanceDetails(Address address)
        {
            return PublicRequest<BalanceDetails>(HttpMethod.Get, $"{BalanceUrl}/details/{address}");
        }

        public ICollection<EntryData> GetData(Address address, Regex regex)
        {
            return PublicRequest<ICollection<EntryData>>(HttpMethod.Get, $"{DataUrl}/{address}?matches={regex}");
        }

        public ICollection<EntryData> GetData(Address address, ICollection<string> keys)
        {
            return PublicRequest<ICollection<EntryData>>(HttpMethod.Post, $"{DataUrl}/{address}", JsonUtils.Serialize(new { keys }));
        }

        public EntryData GetData(Address address, string key)
        {
            return PublicRequest<EntryData>(HttpMethod.Get, $"{DataUrl}/{address}/{key}");
        }

        public ICollection<EntryData> GetData(Address address)
        {
            return PublicRequest<ICollection<EntryData>>(HttpMethod.Get, $"{DataUrl}/{address}");
        }

        public long GetEffectiveBalance(Address address)
        {
            return PublicRequest<AddressBalance>(HttpMethod.Get, $"{EffectiveBalanceUrl}/{address}").Balance;
        }

        public long GetEffectiveBalance(Address address, int confirmations)
        {
            return PublicRequest<AddressBalance>(HttpMethod.Get, $"{EffectiveBalanceUrl}/{address}/{confirmations}").Balance;
        }

        public ScriptInfo GetScriptInfo(Address address)
        {
            return PublicRequest<ScriptInfo>(HttpMethod.Get, $"{ScriptInfoUrl}/{address}");
        }

        public ScriptMeta GetScriptMeta(Address address)
        {
            return PublicRequest<ScriptMeta>(HttpMethod.Get, $"{ScriptInfoUrl}/{address}/meta");
        }
    }
}