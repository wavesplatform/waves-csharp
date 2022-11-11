using Waves.NET.Transactions;

namespace Waves.NET.Leasing
{
    public class LeasingSection : SectionBase, ILeasingSection
    {
        public LeasingSection(HttpClient httpClient, byte chainId) : base(httpClient, "leasing", chainId) { }

        /// <summary>
        /// Get all active leases involving a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns>Lease info</returns>
        public ICollection<LeaseInfo> GetActiveLeases(string address)
        {
            return PublicRequest<ICollection<LeaseInfo>>(HttpMethod.Get, $"active/{address}");
        }

        /// <summary>
        /// Get lease parameters by lease ID
        /// </summary>
        /// <param name="leaseId">Lease ID</param>
        /// <returns></returns>
        public LeaseInfo GetLeaseInfo(string leaseId)
        {
            return PublicRequest<LeaseInfo>(HttpMethod.Get, $"info/{leaseId}");
        }

        /// <summary>
        /// Get lease parameters by lease IDs
        /// </summary>
        /// <param name="leaseIds">Lease IDs</param>
        /// <returns></returns>
        public ICollection<LeaseInfo> GetLeasesInfo(ICollection<string> leaseIds)
        {
            var jsonBody = SerializeObject(new { ids = leaseIds });
            return PublicRequest<ICollection<LeaseInfo>>(HttpMethod.Post, $"info", jsonBody);
        }

        /// <summary>
        /// Get lease parameters by lease IDs
        /// </summary>
        /// <param name="leaseIds">Lease IDs</param>
        /// <returns></returns>
        public ICollection<LeaseInfo> GetLeasesInfo(params string[] leaseIds)
        {
            var jsonBody = SerializeObject(new { ids = leaseIds });
            return PublicRequest<ICollection<LeaseInfo>>(HttpMethod.Post, $"info", jsonBody);
        }
    }
}