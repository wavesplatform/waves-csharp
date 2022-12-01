using WavesLabs.Node.Client.ReturnTypes;
using WavesLabs.Node.Transactions.Common;
using WavesLabs.Node.Transactions.Utils;

namespace WavesLabs.Node.Client.Sections
{
    public class AssetsSection : SectionBase, IAssetsSection
    {
        public AssetsSection(HttpClient httpClient) : base(httpClient, "assets") { }

        public AssetDistribution GetAssetDistribution(AssetId assetId, int height, int limit = 1000, string? after = null)
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

        public long GetAssetBalance(string address, AssetId? assetId)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"balance/{address}/{assetId}").balance;
        }

        public AssetDetails GetAssetDetails(AssetId? assetId, bool full = false)
        {
            return PublicRequest<AssetDetails>(HttpMethod.Get, $"details/{assetId}?full={full}");
        }

        public ICollection<AssetDetails> GetAssetDetails(ICollection<AssetId?> assetIds, bool full = false)
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