using System.Text.RegularExpressions;
using Waves.NET.Addresses.ReturnTypes;
using Waves.NET.Transactions;

namespace Waves.NET.Addresses
{
    public interface IAddressesSection
    {
        ICollection<string> GetAddresses();
        ICollection<string> GetAddresses(int from, int to);
        BalanceDetails GetBalanceDetails(string address);
        ICollection<AddressBalance> GetBalances(ICollection<string> addresses, int height, string asset);
        long GetBalance(string address);
        long GetBalance(string address, int confirmations);
        ICollection<EntryData> GetData(string address);
        ICollection<EntryData> GetData(string address, ICollection<string> keys);
        ICollection<EntryData> GetData(string address, Regex regex);
        EntryData GetData(string address, string key);
        long GetEffectiveBalance(string address);
        long GetEffectiveBalance(string address, int confirmations);
        ScriptInfo GetScriptInfo(string address);
        ScriptMeta GetScriptMeta(string address);
    }
}