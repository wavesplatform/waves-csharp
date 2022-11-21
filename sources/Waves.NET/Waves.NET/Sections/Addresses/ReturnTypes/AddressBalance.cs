using Newtonsoft.Json;

namespace Waves.NET.Addresses
{
    public record AddressBalance
    {
        [JsonProperty("id")]
        public string Address { get; init; } = null!;
        public int Confirmations { get; init; }
        public long Balance { get; init; }
    }
}
