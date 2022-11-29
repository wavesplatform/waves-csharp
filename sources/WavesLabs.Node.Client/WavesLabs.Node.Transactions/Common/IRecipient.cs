using Newtonsoft.Json;
using WavesLabs.Node.Transactions.JsonConverters;

namespace WavesLabs.Node.Transactions.Common
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
