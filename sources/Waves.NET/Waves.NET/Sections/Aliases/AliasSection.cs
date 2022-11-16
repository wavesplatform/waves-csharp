using Waves.NET.Transactions.Common;

namespace Waves.NET.Aliases
{
    public class AliasSection : SectionBase, IAliasSection
    {
        public AliasSection(HttpClient httpClient) : base(httpClient, "alias") { }

        /// <summary>
        /// Get a list of aliases associated with a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        public ICollection<string> GetAliasesByAddress(Address address)
        {
            return PublicRequest<ICollection<string>>(HttpMethod.Get, "by-address/" + address);
        }

        /// <summary>
        /// Get an address associated with a given alias. Alias should be plain text without an 'alias' prefix and chain ID
        /// </summary>
        /// <param name="alias">Alias</param>
        /// <returns></returns>
        public Address GetAddressByAlias(string alias)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, "by-alias/" + alias).address;
        }
    }
}