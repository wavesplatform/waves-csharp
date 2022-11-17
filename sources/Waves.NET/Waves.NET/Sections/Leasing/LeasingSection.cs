using Waves.NET.Transactions;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Leasing
{
    public class LeasingSection : SectionBase, ILeasingSection
    {
        public LeasingSection(HttpClient httpClient) : base(httpClient, "leasing") { }

        public ICollection<LeaseInfo> GetActiveLeases(string address)
        {
            return PublicRequest<ICollection<LeaseInfo>>(HttpMethod.Get, $"active/{address}");
        }

        public LeaseInfo GetLeaseInfo(string leaseId)
        {
            return PublicRequest<LeaseInfo>(HttpMethod.Get, $"info/{leaseId}");
        }

        public ICollection<LeaseInfo> GetLeasesInfo(ICollection<string> leaseIds)
        {
            var jsonBody = JsonUtils.Serialize(new { ids = leaseIds });
            return PublicRequest<ICollection<LeaseInfo>>(HttpMethod.Post, $"info", jsonBody);
        }

        public ICollection<LeaseInfo> GetLeasesInfo(params string[] leaseIds)
        {
            var jsonBody = JsonUtils.Serialize(new { ids = leaseIds });
            return PublicRequest<ICollection<LeaseInfo>>(HttpMethod.Post, $"info", jsonBody);
        }
    }
}