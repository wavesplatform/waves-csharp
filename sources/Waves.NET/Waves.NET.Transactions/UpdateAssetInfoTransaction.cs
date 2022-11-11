namespace Waves.NET.Transactions
{
    public class UpdateAssetInfoTransaction : Transaction, IUpdateAssetInfoTransaction
    {
        public const int TYPE = 17;
        public const int LatestVersion = 1;
        public const int MinFee = 100000;

        public string AssetId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }

    public interface IUpdateAssetInfoTransaction : INonGenesisTransaction
    {
        string AssetId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}