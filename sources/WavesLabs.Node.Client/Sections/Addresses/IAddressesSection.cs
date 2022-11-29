using System.Text.RegularExpressions;
using WavesLabs.Node.Client.ReturnTypes;
using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Client.Sections
{
    public interface IAddressesSection
    {
        /// <summary>
        /// Get a list of account addresses in the <see href="https://docs.waves.tech/en/waves-node/how-to-work-with-node-wallet">node wallet</see>
        /// </summary>
        /// <returns></returns>
        ICollection<Address> GetAddresses();

        /// <summary>
        /// Get a list addresses in the <see href="https://docs.waves.tech/en/waves-node/how-to-work-with-node-wallet">node wallet</see> by a given range of indices. Max range {from}-{to} is 100 addresses
        /// </summary>
        /// <returns></returns>
        ICollection<Address> GetAddresses(int from, int to);

        /// <summary>
        /// Get regular balances for multiple addresses. Max number of addresses is set by <c>waves.rest-api.transactions-by-address-limit</c>, 1000 by default
        /// </summary>
        /// <returns></returns>
        ICollection<AddressBalance> GetBalances(ICollection<Address> addresses, int height);

        /// <summary>
        /// Get regular balances for multiple addresses. Max number of addresses is set by <c>waves.rest-api.transactions-by-address-limit</c>, 1000 by default
        /// </summary>
        /// <returns></returns>
        ICollection<AddressBalance> GetBalances(ICollection<Address> addresses);

        /// <summary>
        /// Get the regular balance in WAVES at a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        long GetBalance(Address address);

        /// <summary>
        /// Get the minimum regular balance at a given address for <c>{confirmations}</c> blocks back from the current height.<br/>
        /// Max number of confirmations is set by <c>waves.db.max-rollback-depth</c>, 2000 by default
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="confirmations">Number of blocks</param>
        /// <returns></returns>
        long GetBalance(Address address, int confirmations);

        /// <summary>
        /// Get the available, regular, generating, and effective balance, see <see href="https://docs.waves.tech/en/blockchain/account/account-balance#account-balance-in-waves">definitions</see>
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        BalanceDetails GetBalanceDetails(Address address);

        /// <summary>
        /// Read account data entries by a regular expression
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="matches">URL encoded (percent-encoded) <see href="https://www.tutorialspoint.com/scala/scala_regular_expressions.htm">regular expression</see> to filter keys</param>
        /// <param name="key">Exact keys to query</param>
        /// <returns></returns>
        ICollection<EntryData> GetData(Address address, Regex regex);

        /// <summary>
        /// Read account data entries by a given keys
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="key">Exact keys to query</param>
        /// <returns></returns>
        ICollection<EntryData> GetData(Address address, ICollection<string> keys);

        /// <summary>
        /// Read account data entries by a given key
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="key">Data key</param>
        /// <returns></returns>
        EntryData GetData(Address address, string key);

        /// <summary>
        /// Read account data entries
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="key">Data key</param>
        /// <returns></returns>
        ICollection<EntryData> GetData(Address address);

        /// <summary>
        /// Get the effective balance in WAVES at a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        long GetEffectiveBalance(Address address);

        /// <summary>
        /// Get the minimum effective balance at a given address for {confirmations} blocks from the current height.<br/>
        /// Max number of confirmations is set by <c>waves.db.max-rollback-depth</c>, 2000 by default
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="confirmations">Number of blocks</param>
        /// <returns></returns>
        long GetEffectiveBalance(Address address, int confirmations);

        /// <summary>
        /// Get an account script or a dApp script with additional info by a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        ScriptInfo GetScriptInfo(Address address);

        /// <summary>
        /// Account script meta
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        ScriptMeta GetScriptMeta(Address address);
    }
}