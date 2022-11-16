using System.Text.RegularExpressions;
using Waves.NET.Addresses.ReturnTypes;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Addresses
{
    public interface IAddressesSection
    {
        ICollection<Address> GetAddresses();
        ICollection<Address> GetAddresses(int from, int to);
        BalanceDetails GetBalanceDetails(Address address);
        ICollection<AddressBalance> GetBalances(ICollection<Address> addresses, int height, string asset);
        long GetBalance(Address address);
        long GetBalance(Address address, int confirmations);
        ICollection<EntryData> GetData(Address address);
        ICollection<EntryData> GetData(Address address, ICollection<string> keys);
        ICollection<EntryData> GetData(Address address, Regex regex);
        EntryData GetData(Address address, string key);
        long GetEffectiveBalance(Address address);
        long GetEffectiveBalance(Address address, int confirmations);
        ScriptInfo GetScriptInfo(Address address);
        ScriptMeta GetScriptMeta(Address address);
    }
}