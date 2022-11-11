using Waves.NET.Assets.ReturnTypes;

namespace Waves.NET.Assets
{
    public interface IAssetsSection
    {
        ICollection<AssetDetails> GetAssetDetails(ICollection<string> assetIds, bool full = false);
        AssetDetails GetAssetDetails(string assetId, bool full = false);
        AssetDistribution GetAssetDistribution(string assetId, int height, int limit = 1000, string? after = null);
        AssetBalance GetAssetsBalance(string address, ICollection<string>? ids = null);
        long GetAssetsBalance(string address, string assetId);
        ICollection<AssetDetails> GetNft(string address, int limit = 1000, string? after = null);
    }
}