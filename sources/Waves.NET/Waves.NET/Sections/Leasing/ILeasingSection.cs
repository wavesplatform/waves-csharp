using Waves.NET.Transactions;

namespace Waves.NET.Leasing
{
    public interface ILeasingSection
    {
        /// <summary>
        /// Get all active leases involving a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <returns>Lease info</returns>
        public ICollection<LeaseInfo> GetActiveLeases(string address);

        /// <summary>
        /// Get lease parameters by lease ID
        /// </summary>
        /// <param name="leaseId">Lease ID</param>
        /// <returns></returns>
        public LeaseInfo GetLeaseInfo(string leaseId);

        /// <summary>
        /// Get lease parameters by lease IDs
        /// </summary>
        /// <param name="leaseIds">Lease IDs</param>
        /// <returns></returns>
        public ICollection<LeaseInfo> GetLeasesInfo(ICollection<string> leaseIds);

        /// <summary>
        /// Get lease parameters by lease IDs
        /// </summary>
        /// <param name="leaseIds">Lease IDs</param>
        /// <returns></returns>
        public ICollection<LeaseInfo> GetLeasesInfo(params string[] leaseIds);
    }
}