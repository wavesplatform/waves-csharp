using Waves.NET.Assets.ReturnTypes;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Assets
{
    public class AssetsSection : SectionBase, IAssetsSection
    {
        public AssetsSection(HttpClient httpClient) : base(httpClient, "assets") { }

        /// <summary>
        /// Get asset balance distribution by addresses at a given height. Max number of addresses is set by <c>waves.rest-api.distribution-address-limit</c>, 1000 by default.<br/>
        /// For pagination, use the field <c>{after}</c>
        /// </summary>
        /// <param name="assetId">Asset ID base58 encoded</param>
        /// <param name="height">For balance at height requests. Max number of blocks back from the current height is set by <c>waves.db.max-rollback-depth</c>, 2000 by default</param>
        /// <param name="limit">Number of addresses to be returned</param>
        /// <param name="after">Address to paginate after</param>
        /// <returns></returns>
        public AssetDistribution GetAssetDistribution(string assetId, int height, int limit = 1000, string? after = null)
        {
            var url = $"{assetId}/distribution/{height}/limit/{limit}";

            if (!string.IsNullOrWhiteSpace(after))
            {
                url += $"?after={after}";
            }

            return PublicRequest<AssetDistribution>(HttpMethod.Get, url);
        }

        /// <summary>
        /// Get account balances in specified assets (excluding WAVES) at a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="ids">Asset IDs base58 encoded</param>
        /// <returns></returns>
        public AssetBalance GetAssetsBalance(string address, ICollection<string>? ids = null)
        {
            var jsonBody = ids is null ? "{{\"ids\":[]}}" : JsonUtils.Serialize(new { ids });
            return PublicRequest<AssetBalance>(HttpMethod.Post, $"balance/{address}", jsonBody);
        }

        /// <summary>
        /// Get the account balance in a given asset. 0 for non-existent asset (use GET /assets/details/{assetId} to check if the asset exists)
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="ids">Asset ID base58 encoded</param>
        /// <returns></returns>
        public long GetAssetsBalance(string address, string assetId)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"balance/{address}/{assetId}").balance;
        }

        /// <summary>
        /// Get detailed information about given asset
        /// </summary>
        /// <param name="id">Asset ID base58 encoded</param>
        /// <param name="full">If true, the response contains scriptDetails for scripted assets. False by default</param>
        /// <returns></returns>
        public AssetDetails GetAssetDetails(string assetId, bool full = false)
        {
            return PublicRequest<AssetDetails>(HttpMethod.Get, $"details/{assetId}?full={full}");
        }

        /// <summary>
        /// Get detailed information about given assets
        /// </summary>
        /// <param name="ids">Asset IDs base58 encoded</param>
        /// <param name="full">If true, the response contains scriptDetails for scripted assets. False by default</param>
        /// <returns></returns>
        public ICollection<AssetDetails> GetAssetDetails(ICollection<string> assetIds, bool full = false)
        {
            var jsonBody = JsonUtils.Serialize(new { ids = assetIds });
            return PublicRequest<ICollection<AssetDetails>>(HttpMethod.Post, $"details?full={full}", jsonBody);
        }

        /// <summary>
        /// Get a list of <see href="https://docs.waves.tech/en/blockchain/token/non-fungible-token">non-fungible tokens</see> at a given address. Max for 1000 tokens.
        /// For pagination, use the field {after}. See <see href="https://docs.waves.tech/en/blockchain/token/#custom-token-parameters">fields descriptions</see>.
        /// Since activation of <see href="https://docs.waves.tech/en/waves-node/features/">feature #15</see> this method returns only tokens that are issued as NFT (amount: 1, decimal places: 0, reissuable: false) after activation of feature #13.
        /// Before activation of feature #15 the method returned all the assets that are issued as NFT.
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="limit">Number of tokens to be returned</param>
        /// <param name="after">ID of the token to paginate after</param>
        /// <returns></returns>
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