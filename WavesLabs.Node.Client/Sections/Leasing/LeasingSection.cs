﻿using WavesLabs.Node.Transactions;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions.Utils;

namespace WavesLabs.Node.Client.Sections
{
    public class LeasingSection : SectionBase, ILeasingSection
    {
        public LeasingSection(HttpClient httpClient) : base(httpClient, "leasing") { }

        public ICollection<LeaseInfo> GetActiveLeases(Address address)
        {
            return PublicRequest<ICollection<LeaseInfo>>(HttpMethod.Get, $"active/{address}");
        }

        public LeaseInfo GetLeaseInfo(Base58s leaseId)
        {
            return PublicRequest<LeaseInfo>(HttpMethod.Get, $"info/{leaseId}");
        }

        public ICollection<LeaseInfo> GetLeasesInfo(ICollection<Base58s> leaseIds)
        {
            var jsonBody = JsonUtils.Serialize(new { ids = leaseIds });
            return PublicRequest<ICollection<LeaseInfo>>(HttpMethod.Post, $"info", jsonBody);
        }

        public ICollection<LeaseInfo> GetLeasesInfo(params Base58s[] leaseIds)
        {
            return GetLeasesInfo(leaseIds.ToList());
        }
    }
}