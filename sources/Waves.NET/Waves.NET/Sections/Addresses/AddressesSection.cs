using System.Text.RegularExpressions;
using Waves.NET.Addresses.ReturnTypes;
using Waves.NET.Transactions;

namespace Waves.NET.Addresses
{
    public class AddressesSection : SectionBase, IAddressesSection
    {
        private const string BalanceUrl = "balance";
        private const string DataUrl = "data";
        private const string EffectiveBalanceUrl = "effectiveBalance";
        private const string ScriptInfoUrl = "scriptInfo";

        public AddressesSection(HttpClient httpClient, byte chainId) : base(httpClient, "addresses", chainId) { }

        /// <summary>
        /// Get a list of account addresses in the <see href="https://docs.waves.tech/en/waves-node/how-to-work-with-node-wallet">node wallet</see>
        /// </summary>
        /// <returns></returns>
        public ICollection<string> GetAddresses()
        {
            return PublicRequest<ICollection<string>>(HttpMethod.Get);
        }

        /// <summary>
        /// Get a list addresses in the <see href="https://docs.waves.tech/en/waves-node/how-to-work-with-node-wallet">node wallet</see> by a given range of indices. Max range {from}-{to} is 100 addresses
        /// </summary>
        /// <returns></returns>
        public ICollection<string> GetAddresses(int from, int to)
        {
            return PublicRequest<ICollection<string>>(HttpMethod.Get, $"seq/{from}/{to}");
        }

        /// <summary>
        /// Get regular balances for multiple addresses. Max number of addresses is set by <c>waves.rest-api.transactions-by-address-limit</c>, 1000 by default
        /// </summary>
        /// <returns></returns>
        public ICollection<AddressBalance> GetBalances(ICollection<string> addresses, int height, string asset)
        {
            return PublicRequest<ICollection<AddressBalance>>(HttpMethod.Post, BalanceUrl, SerializeObject(new { addresses, height, asset }));
        }

        /// <summary>
        /// Get the regular balance in WAVES at a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        public long GetBalance(string address)
        {
            var result = PublicRequest<AddressBalance>(HttpMethod.Get, BalanceUrl + "/" + address);
            return result.Balance;
        }

        /// <summary>
        /// Get the minimum regular balance at a given address for <c>{confirmations}</c> blocks back from the current height.<br/>
        /// Max number of confirmations is set by <c>waves.db.max-rollback-depth</c>, 2000 by default
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="confirmations">Number of blocks</param>
        /// <returns></returns>
        public long GetBalance(string address, int confirmations)
        {
            var result = PublicRequest<AddressBalance>(HttpMethod.Get, $"{BalanceUrl}/{address}/{confirmations}");
            return result.Balance;

        }

        /// <summary>
        /// Get the available, regular, generating, and effective balance, see <see href="https://docs.waves.tech/en/blockchain/account/account-balance#account-balance-in-waves">definitions</see>
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        public BalanceDetails GetBalanceDetails(string address)
        {
            return PublicRequest<BalanceDetails>(HttpMethod.Get, $"{BalanceUrl}/details/{address}");
        }

        /// <summary>
        /// Read account data entries by a regular expression
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="matches">URL encoded (percent-encoded) <see href="https://www.tutorialspoint.com/scala/scala_regular_expressions.htm">regular expression</see> to filter keys</param>
        /// <param name="key">Exact keys to query</param>
        /// <returns></returns>
        public ICollection<EntryData> GetData(string address, Regex regex)
        {
            return PublicRequest<ICollection<EntryData>>(HttpMethod.Get, $"{DataUrl}/{address}?matches={regex}");
        }

        /// <summary>
        /// Read account data entries by a given keys
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="key">Exact keys to query</param>
        /// <returns></returns>
        public ICollection<EntryData> GetData(string address, ICollection<string> keys)
        {
            return PublicRequest<ICollection<EntryData>>(HttpMethod.Post, $"{DataUrl}/{address}", SerializeObject(new { keys }));
        }

        /// <summary>
        /// Read account data entries by a given key
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="key">Data key</param>
        /// <returns></returns>
        public EntryData GetData(string address, string key)
        {
            return PublicRequest<EntryData>(HttpMethod.Get, $"{DataUrl}/{address}/{key}");
        }

        /// <summary>
        /// Read account data entries
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="key">Data key</param>
        /// <returns></returns>
        public ICollection<EntryData> GetData(string address)
        {
            return PublicRequest<ICollection<EntryData>>(HttpMethod.Get, $"{DataUrl}/{address}");
        }

        /// <summary>
        /// Get the effective balance in WAVES at a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        public long GetEffectiveBalance(string address)
        {
            return PublicRequest<AddressBalance>(HttpMethod.Get, $"{EffectiveBalanceUrl}/{address}").Balance;
        }

        /// <summary>
        /// Get the minimum effective balance at a given address for {confirmations} blocks from the current height.<br/>
        /// Max number of confirmations is set by <c>waves.db.max-rollback-depth</c>, 2000 by default
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="confirmations">Number of blocks</param>
        /// <returns></returns>
        public long GetEffectiveBalance(string address, int confirmations)
        {
            return PublicRequest<AddressBalance>(HttpMethod.Get, $"{EffectiveBalanceUrl}/{address}/{confirmations}").Balance;
        }

        /// <summary>
        /// Get an account script or a dApp script with additional info by a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        public ScriptInfo GetScriptInfo(string address)
        {
            return PublicRequest<ScriptInfo>(HttpMethod.Get, $"{ScriptInfoUrl}/{address}");
        }

        /// <summary>
        /// Account script meta
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        public ScriptMeta GetScriptMeta(string address)
        {
            return PublicRequest<ScriptMeta>(HttpMethod.Get, $"{ScriptInfoUrl}/{address}/meta");
        }
    }
}