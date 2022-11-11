using Newtonsoft.Json;
using Waves.NET.Transactions.JsonConverters;

namespace Waves.NET.Transactions
{
    [JsonConverter(typeof(DataEntryJsonConverter))]
    public abstract record EntryData
    {
        public string Key { get; init; } = null!;
    }
}
