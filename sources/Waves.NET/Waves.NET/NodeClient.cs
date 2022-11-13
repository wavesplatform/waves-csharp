using Waves.NET.Addresses;
using Waves.NET.Alias;
using Waves.NET.Assets;
using Waves.NET.Blockchain;
using Waves.NET.Blocks;
using Waves.NET.Debug;
using Waves.NET.Leasing;
using Waves.NET.Node;
using Waves.NET.Transactions;
using Waves.NET.Utils;

namespace Waves.NET
{
    public sealed class NodeClient : INodeClient
    {
        private readonly HttpClient _httpClient;

        public byte ChainId { get; init; }

        public IAddressesSection Addresses { get; init; }
        public IAliasSection Alias { get; init; }
        public IAssetsSection Assets { get; init; }
        public IBlockchainSection Blockchain { get; init; }
        public IBlocksSection Blocks { get; init; }
        public IDebugSection Debug { get; init; }
        public INodeSection Node { get; init; }
        public ITransactionsSection Transactions { get; init; }
        public IUtilsSection Utils { get; init; }
        public ILeasingSection Leasing { get; init; }

        public static INodeClient Create(string nodeUri)
        {
            return new NodeClient(nodeUri);
        }

        public NodeClient(string nodeUri)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(nodeUri);

            Addresses = new AddressesSection(_httpClient);
            Alias = new AliasSection(_httpClient);
            Assets = new AssetsSection(_httpClient);
            Blockchain = new BlockchainSection(_httpClient);
            Blocks = new BlocksSection(_httpClient);
            Debug = new DebugSection(_httpClient);
            Node = new NodeSection(_httpClient);
            Transactions = new TransactionsSection(_httpClient);
            Utils = new UtilsSection(_httpClient);
            Leasing = new LeasingSection(_httpClient);

            WavesConfig.ChainId = Addresses.GetAddresses().First().ChainId;
            ChainId = WavesConfig.ChainId;
        }
    }
}