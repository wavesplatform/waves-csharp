namespace Waves.NET.Assets
{
    public class ScriptDetails : IEquatable<ScriptDetails?>
    {
        public int ScriptComplexity { get; set; }
        public string Script { get; set; } = null!;
        //public string ScriptText { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as ScriptDetails is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as ScriptDetails);
        }

        public bool Equals(ScriptDetails? other)
        {
            return other is not null &&
                ScriptComplexity == other.ScriptComplexity &&
                Script == other.Script;
                //&& ScriptText == other.ScriptText;
        }

        public override int GetHashCode() => HashCode.Combine(ScriptComplexity, Script /*, ScriptText*/);
        public static bool operator ==(ScriptDetails? left, ScriptDetails? right) => EqualityComparer<ScriptDetails>.Default.Equals(left, right);
        public static bool operator !=(ScriptDetails? left, ScriptDetails? right) => !(left == right);
    }
}
