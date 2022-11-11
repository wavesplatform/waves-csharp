namespace Waves.NET.Addresses.ReturnTypes
{
    public record ScriptInfo
    {
        public string Address { get; init; } = null!;
        public string? Script { get; init; }
        public string? ScriptText { get; init; }
        public long Complexity { get; init; }
        public long VerifierComplexity { get; init; }
        public object CallableComplexities { get; init; } = null!; //TODO! object?
        public long ExtraFee { get; init; }
    }
}
