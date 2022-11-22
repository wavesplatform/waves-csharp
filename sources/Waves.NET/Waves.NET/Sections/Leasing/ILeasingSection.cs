using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Sections
{
    public interface ILeasingSection
    {
        /// <summary>
        /// Get all active leases involving a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns>Lease info</returns>
        public ICollection<LeaseInfo> GetActiveLeases(Address address);

        /// <summary>
        /// Get lease parameters by lease ID
        /// </summary>
        /// <param name="leaseId">Lease ID</param>
        /// <returns></returns>
        public LeaseInfo GetLeaseInfo(Base58s leaseId);

        /// <summary>
        /// Get lease parameters by lease IDs
        /// </summary>
        /// <param name="leaseIds">Lease IDs</param>
        /// <returns></returns>
        public ICollection<LeaseInfo> GetLeasesInfo(ICollection<Base58s> leaseIds);

        /// <summary>
        /// Get lease parameters by lease IDs
        /// </summary>
        /// <param name="leaseIds">Lease IDs</param>
        /// <returns></returns>
        public ICollection<LeaseInfo> GetLeasesInfo(params Base58s[] leaseIds);
    }
}