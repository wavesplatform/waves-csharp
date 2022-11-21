using Waves.NET.Assets;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Assets
{
    public class AssetsSection : SectionBase, IAssetsSection
    {
        public AssetsSection(HttpClient httpClient) : base(httpClient, "assets") { }

        public AssetDistribution GetAssetDistribution(Base58s assetId, int height, int limit = 1000, string? after = null)
        {
            var url = $"{assetId}/distribution/{height}/limit/{limit}";

            if (!string.IsNullOrWhiteSpace(after))
            {
                url += $"?after={after}";
            }

            return PublicRequest<AssetDistribution>(HttpMethod.Get, url);
        }

        public AssetBalance GetAssetsBalance(string address, ICollection<string>? ids = null)
        {
            var jsonBody = JsonUtils.Serialize(new { ids = ids ?? new List<string>() });
            return PublicRequest<AssetBalance>(HttpMethod.Post, $"balance/{address}", jsonBody);
        }

        public long GetAssetsBalance(string address, Base58s? assetId)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"balance/{address}/{assetId}").balance;
        }

        public AssetDetails GetAssetDetails(Base58s? assetId, bool full = false)
        {
            return PublicRequest<AssetDetails>(HttpMethod.Get, $"details/{assetId}?full={full}");
        }

        public ICollection<AssetDetails> GetAssetDetails(ICollection<Base58s?> assetIds, bool full = false)
        {
            var jsonBody = JsonUtils.Serialize(new { ids = assetIds });
            return PublicRequest<ICollection<AssetDetails>>(HttpMethod.Post, $"details?full={full}", jsonBody);
        }

        public ICollection<AssetDetails> GetNft(string address, int limit = 1000, string? after = null)
        {
            var url = $"nft/{address}/limit/{limit}";

            if (!string.IsNullOrWhiteSpace(after))
            {
                url += $"?after={after}";
            }

            return PublicRequest<ICollection<AssetDetails>>(HttpMethod.Get, url);
        }
    }
}