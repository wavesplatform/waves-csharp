using Newtonsoft.Json;

namespace Waves.NET.Blocks.ReturnTypes
{
    public record NxtConsensus
    {
        [JsonProperty("base-target")]
        public long BaseTarget { get; init; }

        [JsonProperty("generation-signature")]
        public string GenerationSignature { get; init; } = null!;
    }
}
