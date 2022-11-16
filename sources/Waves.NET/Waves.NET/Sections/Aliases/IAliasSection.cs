using Waves.NET.Transactions.Common;

namespace Waves.NET.Aliases
{
    public interface IAliasSection
    {
        Address GetAddressByAlias(string alias);
        ICollection<string> GetAliasesByAddress(Address address);
    }
}