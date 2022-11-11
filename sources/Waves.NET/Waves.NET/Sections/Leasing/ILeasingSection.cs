using Waves.NET.Transactions;

namespace Waves.NET.Leasing
{
    public interface ILeasingSection
    {
        ICollection<LeaseInfo> GetActiveLeases(string address);
        LeaseInfo GetLeaseInfo(string leaseId);
        ICollection<LeaseInfo> GetLeasesInfo(ICollection<string> leaseIds);
        ICollection<LeaseInfo> GetLeasesInfo(params string[] leaseIds);
    }
}