using Waves.NET.ReturnTypes;
using Waves.NET.Transactions.Common;

namespace Waves.NET.Sections
{
    public interface IAssetsSection
    {
        /// <summary>
        /// Get asset balance distribution by addresses at a given height. Max number of addresses is set by <c>waves.rest-api.distribution-address-limit</c>, 1000 by default.<br/>
        /// For pagination, use the field <c>{after}</c>
        /// </summary>
        /// <param name="assetId">Asset ID base58 encoded</param>
        /// <param name="height">For balance at height requests. Max number of blocks back from the current height is set by <c>waves.db.max-rollback-depth</c>, 2000 by default</param>
        /// <param name="limit">Number of addresses to be returned</param>
        /// <param name="after">Address to paginate after</param>
        /// <returns></returns>
        public AssetDistribution GetAssetDistribution(Base58s assetId, int height, int limit = 1000, string? after = null);

        /// <summary>
        /// Get account balances in specified assets (excluding WAVES) at a given address
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="ids">Asset IDs base58 encoded</param>
        /// <returns></returns>
        public AssetBalance GetAssetsBalance(string address, ICollection<string>? ids = null);

        /// <summary>
        /// Get the account balance in a given asset. 0 for non-existent asset (use GET /assets/details/{assetId} to check if the asset exists)
        /// </summary>
        /// <param name="address">Address base58 encoded</param>
        /// <param name="ids">Asset ID base58 encoded</param>
        /// <returns></returns>
        public long GetAssetsBalance(string address, Base58s? assetId);

        /// <summary>
        /// Get detailed information about given asset
        /// </summary>
        /// <param name="id">Asset ID base58 encoded</param>
        /// <param name="full">If true, the response contains scriptDetails for scripted assets. False by default</param>
        /// <returns></returns>
        public AssetDetails GetAssetDetails(Base58s? assetId, bool full = false);

        /// <summary>
        /// Get detailed information about given assets
        /// </summary>
        /// <param name="ids">Asset IDs base58 encoded</param>
        /// <param name="full">If true, the response contains scriptDetails for scripted assets. False by default</param>
        /// <returns></returns>
        public ICollection<AssetDetails> GetAssetDetails(ICollection<Base58s?> assetIds, bool full = false);

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
        public ICollection<AssetDetails> GetNft(string address, int limit = 1000, string? after = null);
    }
}