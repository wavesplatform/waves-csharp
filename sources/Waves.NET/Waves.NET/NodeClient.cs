using System.Diagnostics;
using System.Text.RegularExpressions;
using Waves.NET.ReturnTypes;
using Waves.NET.Sections;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Info;

namespace Waves.NET
{
    public sealed class NodeClient : INode, INodeSections
    {
        private readonly HttpClient _httpClient;

        public byte ChainId { get; init; }

        public IAddressesSection Addresses { get; init; }
        public IAliasesSection Aliases { get; init; }
        public IAssetsSection Assets { get; init; }
        public IBlockchainSection Blockchain { get; init; }
        public IBlocksSection Blocks { get; init; }
        public IDebugSection Debug { get; init; }
        public INodeSection Node { get; init; }
        public ITransactionsSection Transactions { get; init; }
        public IUtilsSection Utils { get; init; }
        public ILeasingSection Leasing { get; init; }

        public static INode Create(string nodeUri)
        {
            return new NodeClient(nodeUri);
        }

        public static INodeSections CreateSections(string nodeUri)
        {
            return new NodeClient(nodeUri);
        }

        public NodeClient(string nodeUri)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(nodeUri);

            Addresses = new AddressesSection(_httpClient);
            Aliases = new AliasSection(_httpClient);
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

        #region Addresses
        public ICollection<Address> GetAddresses() => Addresses.GetAddresses();
        public ICollection<Address> GetAddresses(int from, int to) => Addresses.GetAddresses(from, to);
        public ICollection<AddressBalance> GetBalances(ICollection<Address> addresses, int height) => Addresses.GetBalances(addresses, height);
        public ICollection<AddressBalance> GetBalances(ICollection<Address> addresses) => Addresses.GetBalances(addresses);
        public long GetBalance(Address address) => Addresses.GetBalance(address);
        public long GetBalance(Address address, int confirmations) => Addresses.GetBalance(address, confirmations);
        public BalanceDetails GetBalanceDetails(Address address) => Addresses.GetBalanceDetails(address);
        public ICollection<EntryData> GetData(Address address, Regex regex) => Addresses.GetData(address, regex);
        public ICollection<EntryData> GetData(Address address, ICollection<string> keys) => Addresses.GetData(address, keys);
        public EntryData GetData(Address address, string key) => Addresses.GetData(address, key);
        public ICollection<EntryData> GetData(Address address) => Addresses.GetData(address);
        public long GetEffectiveBalance(Address address) => Addresses.GetEffectiveBalance(address);
        public long GetEffectiveBalance(Address address, int confirmations) => Addresses.GetEffectiveBalance(address, confirmations);
        public ScriptInfo GetScriptInfo(Address address) => Addresses.GetScriptInfo(address);
        public ScriptMeta GetScriptMeta(Address address) => Addresses.GetScriptMeta(address);
        #endregion

        #region Aliases
        public Address GetAddressByAlias(string alias) => Aliases.GetAddressByAlias(alias);
        public ICollection<Alias> GetAliasesByAddress(Address address) => Aliases.GetAliasesByAddress(address);
        #endregion

        #region Assets
        public AssetDistribution GetAssetDistribution(Base58s assetId, int height, int limit = 1000, string? after = null) =>
            Assets.GetAssetDistribution(assetId, height, limit, after);
        public AssetBalance GetAssetsBalance(string address, ICollection<string>? ids = null) => Assets.GetAssetsBalance(address, ids);
        public long GetAssetsBalance(string address, Base58s? assetId) => Assets.GetAssetsBalance(address, assetId);
        public AssetDetails GetAssetDetails(Base58s? assetId, bool full = false) => Assets.GetAssetDetails(assetId, full);
        public ICollection<AssetDetails> GetAssetDetails(ICollection<Base58s?> assetIds, bool full = false) => Assets.GetAssetDetails(assetIds, full);
        public ICollection<AssetDetails> GetNft(string address, int limit = 1000, string? after = null) => Assets.GetNft(address, limit, after);
        #endregion

        #region Blockchain
        public BlockchainRewards GetBlockchainRewards() => Blockchain.GetBlockchainRewards();
        public BlockchainRewards GetBlockchainRewards(int height) => Blockchain.GetBlockchainRewards(height);
        #endregion

        #region Blocks
        public Block GetBlock(string blockId) => Blocks.GetBlock(blockId);
        public ICollection<Block> GetBlocksGeneratedBy(string generator, int fromHeight, int toHeight) => Blocks.GetBlocksGeneratedBy(generator, fromHeight, toHeight);
        public Block GetBlock(int height) => Blocks.GetBlock(height);
        public long GetBlocksDelay(string startBlockId, int blocksNum) => Blocks.GetBlocksDelay(startBlockId, blocksNum);
        public BlockHeader GetBlockHeaders(string blockId) => Blocks.GetBlockHeaders(blockId);
        public BlockHeader GetBlockHeaders(int height) => Blocks.GetBlockHeaders(height);
        public BlockHeader GetLastBlockHeaders() => Blocks.GetLastBlockHeaders();
        public ICollection<BlockHeader> GetBlocksHeaders(int fromHeight, int toHeight) => Blocks.GetBlocksHeaders(fromHeight, toHeight);
        public int GetHeight() => Blocks.GetHeight();
        public int GetBlockHeight(string blockId) => Blocks.GetBlockHeight(blockId);
        public int GetBlockHeight(long timestamp) => Blocks.GetBlockHeight(timestamp);
        public Block GetLastBlock() => Blocks.GetLastBlock();
        public ICollection<Block> GetBlocks(int fromHeight, int toHeight) => Blocks.GetBlocks(fromHeight, toHeight);
        public Block GetGenesisBlock() => Blocks.GetGenesisBlock();
        #endregion

        #region Debug
        public ICollection<HistoryBalance> GetBalanceHistory(string address) => Debug.GetBalanceHistory(address);
        public TransactionValidationResult ValidateTransaction<T>(T transaction) where T : Transaction => Debug.ValidateTransaction(transaction);
        #endregion

        #region Leasing
        public ICollection<LeaseInfo> GetActiveLeases(Address address) => Leasing.GetActiveLeases(address);
        public LeaseInfo GetLeaseInfo(Base58s leaseId) => Leasing.GetLeaseInfo(leaseId);
        public ICollection<LeaseInfo> GetLeasesInfo(ICollection<Base58s> leaseIds) => Leasing.GetLeasesInfo(leaseIds);
        public ICollection<LeaseInfo> GetLeasesInfo(params Base58s[] leaseIds) => Leasing.GetLeasesInfo(leaseIds);
        #endregion

        #region Node
        public NodeStatus GetNodeStatus() => Node.GetNodeStatus();
        public string GetVersion() => Node.GetVersion();
        #endregion

        #region Transactions
        public TransactionFeeAmount CalculateTransactionFee<T>(T transaction) where T : Transaction => Transactions.CalculateTransactionFee(transaction);
        public ICollection<TransactionInfo> GetTransactionsByAddress(Address address, int limit = 1000, Base58s? afterTxId = null) =>
            Transactions.GetTransactionsByAddress(address, limit, afterTxId);
        public T Broadcast<T>(T transaction, bool trace = false) where T : Transaction => Transactions.Broadcast(transaction, trace);
        public ICollection<TransactionInfo> GetTransactionsInfo(ICollection<Base58s> ids) => Transactions.GetTransactionsInfo(ids);
        public ICollection<T> GetTransactionsInfo<T>(ICollection<Base58s> ids) where T : TransactionInfo => Transactions.GetTransactionsInfo<T>(ids);
        public TransactionInfo GetTransactionInfo(Base58s id) => Transactions.GetTransactionInfo(id);
        public T GetTransactionInfo<T>(Base58s id) where T : TransactionInfo => Transactions.GetTransactionInfo<T>(id);
        public ICollection<TransactionStatus> GetTransactionsStatus(ICollection<Base58s> ids) => Transactions.GetTransactionsStatus(ids);
        public TransactionStatus GetTransactionStatus(Base58s id) => Transactions.GetTransactionStatus(id);
        public ICollection<Transaction> GetUnconfirmedTransactions() => Transactions.GetUnconfirmedTransactions();
        public Transaction GetUnconfirmedTransaction(Base58s id) => Transactions.GetUnconfirmedTransaction(id);
        public int GetUtxSize() => Transactions.GetUtxSize();
        #endregion

        #region Utils
        public string GenerateRandomSeed() => Utils.GenerateRandomSeed();
        public string GenerateRandomSeedOfLength(int length) => Utils.GenerateRandomSeedOfLength(length);
        public NodeTime GetNodeTimeUtc() => Utils.GetNodeTimeUtc();
        public string GetFastHash(string message) => Utils.GetFastHash(message);
        public string GetSecureHash(string message) => Utils.GetSecureHash(message);
        public ScriptInfo CompileScript(string script, bool compact = false) => Utils.CompileScript(script, compact);
        public ScriptInfo GetScriptCompiledCodeWithImports(string scriptWithImports) => Utils.GetScriptCompiledCodeWithImports(scriptWithImports);
        public string DecompileScript(string code) => Utils.DecompileScript(code);
        public ScriptInfo GetScriptEstimate(string code) => Utils.GetScriptEstimate(code);
        public ScriptEvaluationResult EvaluateScript(string address, ScriptEvaluationExpression evaluationExpression) => Utils.EvaluateScript(address, evaluationExpression);
        #endregion

        #region Waitings
        public void WaitForTransactions(ICollection<Base58s> transactionIds, uint waitForSeconds = 60)
        {
            const int PollingIntervalInMillis = 1000;
            uint waitForMillis = waitForSeconds * 1000;
            Exception? lastException = null;

            var sw = Stopwatch.StartNew();
            while (true)
            {
                try
                {
                    var statuses = GetTransactionsStatus(transactionIds);
                    if (statuses.All(x => x.Status.Equals("confirmed", StringComparison.OrdinalIgnoreCase))) return;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    Thread.Sleep(PollingIntervalInMillis);
                }

                if (sw.ElapsedMilliseconds > waitForMillis)
                    throw new Exception($"WaitForTransaction: Operation timeout ({waitForSeconds} seconds). Last exception: {lastException}");
            }
        }

        public T WaitForTransaction<T>(Base58s? transactionId, uint waitForSeconds = 60) where T : TransactionInfo
        {
            return (T) WaitForTransaction(transactionId, waitForSeconds);
        }

        public TransactionInfo WaitForTransaction(Base58s? transactionId, uint waitForSeconds = 60)
        {
            if (transactionId is null)
                throw new ArgumentNullException($"WaitForTransaction: transaction ID cannot be null");

            const int PollingIntervalInMillis = 100;
            uint waitForMillis = waitForSeconds * 1000;
            Exception? lastException;

            var sw = Stopwatch.StartNew();
            while (true)
            {
                try
                {
                    return GetTransactionInfo(transactionId);
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    Thread.Sleep(PollingIntervalInMillis);
                }

                if (sw.ElapsedMilliseconds > waitForMillis)
                    throw new Exception($"WaitForTransaction: Operation timeout ({waitForSeconds} seconds). Last exception: {lastException}");
            }
        }


        public int WaitForHeight(int target, uint waitForSeconds = 60)
        {
            int start = GetHeight();
            int prev = start;
            const int PollingIntervalInMillis = 100;
            uint waitForMillis = waitForSeconds * 1000;

            var sw = Stopwatch.StartNew();

            while (true)
            {
                try
                {
                    int current = GetHeight();

                    if (current >= target)
                    {
                        return current;
                    }
                    else if (current > prev)
                    {
                        prev = current;
                    }
                }
                catch
                {
                    Thread.Sleep(PollingIntervalInMillis);
                }

                if (sw.ElapsedMilliseconds > waitForMillis)
                    throw new Exception($"WaitForHeight: timeout reached ({waitForSeconds} seconds)");
            }
        }

        public int WaitBlocks(int blocksCount, uint waitingInSeconds = 60)
        {
            var height = GetHeight();
            return WaitForHeight(height + blocksCount, waitingInSeconds);
        }

        public int WaitBlocks(int blocksCount)
        {
            return WaitBlocks(blocksCount, 60 * 3);
        }
        #endregion
    }
}