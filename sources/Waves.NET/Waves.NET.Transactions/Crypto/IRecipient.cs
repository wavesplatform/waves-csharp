using Newtonsoft.Json;
using Waves.NET.Transactions.JsonConverters;

namespace Waves.NET.Transactions.Crypto
{
    [JsonConverter(typeof(RecipientJsonConverter))]
    public interface IRecipient
    {
        byte Type { get; }
        byte ChainId { get; }
        byte[] Bytes { get; }
        string ToString();
    }
}
