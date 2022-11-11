namespace Waves.NET.Addresses.ReturnTypes
{
    public record ScriptMeta
    {
        public string Version { get; init; } = null!;
        public bool IsArrayArguments { get; init; }
        public IDictionary<string, ICollection<NameTypePair>> CallableFuncTypes { get; init; } = null!;
    }
}