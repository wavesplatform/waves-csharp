namespace Waves.NET.Transactions
{
    public record WavesNodeClientAppSettings
    {
        public string NodeEndpoint { get; init; } = null!;
        public byte ChainId { get; init; }

        public WavesNodeClientAppSettings() { }

        public WavesNodeClientAppSettings(string endPoint, byte chainId) {
            NodeEndpoint = endPoint;
            ChainId = chainId;
        }
    }
}
