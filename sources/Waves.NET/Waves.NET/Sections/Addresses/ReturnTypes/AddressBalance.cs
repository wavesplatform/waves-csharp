using Newtonsoft.Json;

namespace Waves.NET.ReturnTypes
{
    public record AddressBalance
    {
        [JsonProperty("id")]
        public string Address { get; init; } = null!;
        public int Confirmations { get; init; }
        public long Balance { get; init; }
    }
}
