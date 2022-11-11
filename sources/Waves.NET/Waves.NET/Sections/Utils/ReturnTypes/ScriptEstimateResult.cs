namespace Waves.NET.Utils.ReturnTypes
{
    public record ScriptEstimateResult : ScriptResult
    {
        public string ScriptText { get; init; } = "";
    }
}