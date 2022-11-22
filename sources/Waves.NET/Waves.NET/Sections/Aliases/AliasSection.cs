using Waves.NET.Transactions.Common;

namespace Waves.NET.Sections
{
    public class AliasSection : SectionBase, IAliasesSection
    {
        public AliasSection(HttpClient httpClient) : base(httpClient, "alias") { }

        public ICollection<Alias> GetAliasesByAddress(Address address)
        {
            return PublicRequest<ICollection<Alias>>(HttpMethod.Get, "by-address/" + address);
        }

        public Address GetAddressByAlias(string alias)
        {
            var result = PublicRequest<dynamic>(HttpMethod.Get, "by-alias/" + alias).address;
            return Address.As((string)result);
        }
    }
}