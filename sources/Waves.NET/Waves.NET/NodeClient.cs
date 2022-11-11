using Microsoft.Extensions.Options;
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

        public NodeClient(IOptions<WavesNodeClientAppSettings> wavesNodeAppSettings)
            : this(wavesNodeAppSettings.Value.NodeEndpoint, wavesNodeAppSettings.Value.ChainId) { }

        public NodeClient(WavesNodeClientAppSettings wavesNodeAppSettings)
            : this(wavesNodeAppSettings.NodeEndpoint, wavesNodeAppSettings.ChainId) { }

        public NodeClient(string nodeUri, byte chainId)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(nodeUri);

            WavesConfig.CurrentChainId = chainId;
            ChainId = chainId;

            Addresses = new AddressesSection(_httpClient, chainId);
            Alias = new AliasSection(_httpClient, chainId);
            Assets = new AssetsSection(_httpClient, chainId);
            Blockchain = new BlockchainSection(_httpClient, chainId);
            Blocks = new BlocksSection(_httpClient, chainId);
            Debug = new DebugSection(_httpClient, chainId);
            Node = new NodeSection(_httpClient, chainId);
            Transactions = new TransactionsSection(_httpClient, chainId);
            Utils = new UtilsSection(_httpClient, chainId);
            Leasing = new LeasingSection(_httpClient, chainId);
        }

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

        public static INodeClient Create(string nodeUri, byte chainId)
        {
            return new NodeClient(nodeUri, chainId);
        }

        public static INodeClient Create(WavesNodeClientAppSettings wavesNodeAppSettings)
        {
            return new NodeClient(wavesNodeAppSettings);
        }
    }
}