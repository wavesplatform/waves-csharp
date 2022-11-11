namespace Waves.NET.Alias
{
    public class AliasSection : SectionBase, IAliasSection
    {
        public AliasSection(HttpClient httpClient, byte chainId) : base(httpClient, "alias", chainId) { }

        /// <summary>
        /// Get a list of aliases associated with a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns></returns>
        public ICollection<string> GetAliasesByAddress(string address)
        {
            return PublicRequest<ICollection<string>>(HttpMethod.Get, "by-address/" + address);
        }

        /// <summary>
        /// Get an address associated with a given alias. Alias should be plain text without an 'alias' prefix and chain ID
        /// </summary>
        /// <param name="alias">Alias</param>
        /// <returns></returns>
        public string GetAddressByAlias(string alias)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, "by-alias/" + alias).address;
        }
    }
}