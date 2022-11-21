using Waves.NET.Transactions.Common;

namespace Waves.NET.Aliases
{
    public interface IAliasesSection
    {
        /// <summary>
        /// Get an address associated with a given alias. Alias should be plain text without an 'alias' prefix and chain ID
        /// </summary>
        /// <param name="alias">Alias</param>
        /// <returns></returns>
        Address GetAddressByAlias(string alias);

        /// <summary>
        /// Get a list of aliases associated with a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        ICollection<Alias> GetAliasesByAddress(Address address);
    }
}