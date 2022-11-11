namespace Waves.NET.Assets.ReturnTypes
{
    public record ScriptDetails
    {
        public int ScriptComplexity { get; set; }
        public string Script { get; set; } = null!;
        public string ScriptText { get; set; } = null!;
    }
}
