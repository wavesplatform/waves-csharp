namespace Waves.NET.Transactions
{
    public abstract class WavesConfig {
        public static byte ChainId { get; set; } = ChainIds.MainNet;
    }
}
