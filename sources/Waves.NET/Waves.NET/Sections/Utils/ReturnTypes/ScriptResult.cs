namespace Waves.NET.Utils.ReturnTypes
{
    public record ScriptResult
    {
        public string Script { get; init; } = "";
        public long Complexity { get; init; }
        public long VerifierComplexity { get; init; }
        public object CallableComplexities { get; init; } //TODO: check docs about
        public long ExtraFee { get; init; }
    }
}