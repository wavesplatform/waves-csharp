namespace Waves.NET.Transactions
{
    public record CallArg
    {
        public CallArgType Type { get; init; }
        public object Value { get; init; } = null!;

        public static CallArg As(CallArgType type, object value) => new CallArg { Type = type, Value = value };
    }
}