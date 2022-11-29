using Newtonsoft.Json;
using WavesLabs.Node.Transactions.JsonConverters;

namespace WavesLabs.Node.Transactions
{
    [JsonConverter(typeof(DataEntryJsonConverter))]
    public abstract record EntryData
    {
        public string Key { get; init; } = null!;
    }
}
