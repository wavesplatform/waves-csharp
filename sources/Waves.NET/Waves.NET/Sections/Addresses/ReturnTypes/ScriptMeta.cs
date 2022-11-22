using Waves.NET.Addresses;
using Waves.NET.Transactions;

namespace Waves.NET.ReturnTypes
{
    public class ScriptMeta : IEquatable<ScriptMeta?>
    {
        public int Version { get; init; }
        public bool IsArrayArguments { get; init; }
        public IDictionary<string, ICollection<NameTypePair>> CallableFuncTypes { get; init; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as ScriptMeta is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ScriptMeta);
        }

        public bool Equals(ScriptMeta? other)
        {
            if (other is null) return false;

            var cftNulls = CallableFuncTypes == null && other.CallableFuncTypes == null;

            return other is not null &&
                   Version == other.Version &&
                   IsArrayArguments == other.IsArrayArguments &&
                    (cftNulls || CallableFuncTypes!.ContentEquals(other.CallableFuncTypes!));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Version, IsArrayArguments, CallableFuncTypes);
        }
    }
}