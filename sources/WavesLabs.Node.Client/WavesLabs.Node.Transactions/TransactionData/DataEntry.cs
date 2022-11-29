using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public abstract record DataEntry : EntryData
    {
        public string Type { get; init; } = null!;

        public static DataEntry AsBinary(string key, Base64s value) => new BinaryEntry { Key = key, Value = value };
        public static DataEntry AsBoolean(string key, bool value) => new BooleanEntry { Key = key, Value = value };
        public static DataEntry AsInteger(string key, long value) => new IntegerEntry { Key = key, Value = value };
        public static DataEntry AsString(string key, string value) => new StringEntry { Key = key, Value = value };
    }
}
