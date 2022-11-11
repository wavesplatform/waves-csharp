namespace Waves.NET.Transactions
{
    public abstract class WavesConfig {
        public static byte CurrentChainId { get; set; } = 84;

        public static WavesNodeClientAppSettings MainNet = new("https://nodes.wavesnodes.com/", 87);            //87 = 'W'
        public static WavesNodeClientAppSettings TestNet = new("https://nodes-testnet.wavesnodes.com/", 84);    //84 = 'T'
        public static WavesNodeClientAppSettings StageNet = new("https://nodes-stagenet.wavesnodes.com/", 83);  //83 = 'S'
    }
}
