namespace Waves.NET.Transactions
{
    public record CallArgs
    {
        public CallArgType Type { get; init; }
        public object Value { get; init; } = null!;
    }
}