using Newtonsoft.Json;

namespace Waves.NET.Blocks.ReturnTypes
{
    public record BlockHeaders
    {
        public long Timestamp { get; init; }
        public int Version { get; init; }
        public int Height { get; init; }
        public long TotalFee { get; init; }
        public string Reference { get; init; } = null!;
        public string Generator { get; init; } = null!;
        public string GeneratorPublicKey { get; init; } = null!;
        public string Signature { get; init; } = null!;
        public string Id { get; init; } = null!;
        [JsonProperty("nxt-consensus")]
        public NxtConsensus NxtConsensus { get; init; } = null!;
        public int Blocksize { get; init; }
        public int TransactionCount { get; init; }
        public ICollection<int> Features { get; init; } = null!;
        public long Reward { get; init; }
        public long DesiredReward { get; init; }
        public string VRF { get; init; } = null!;
        public string TransactionsRoot { get; init; } = null!;
    }
}