using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class CallArg : IEquatable<CallArg?>
    {
        public CallArgType Type { get; init; }
        public object Value { get; init; } = null!;

        public static CallArg AsByteArray(Base64s value) => new CallArg { Type = CallArgType.ByteArray, Value = value };
        public static CallArg AsBoolean(bool value) => new CallArg { Type = CallArgType.Boolean, Value = value };
        public static CallArg AsInteger(long value) => new CallArg { Type = CallArgType.Integer, Value = value };
        public static CallArg AsString(string value) => new CallArg { Type = CallArgType.String, Value = value };
        public static CallArg AsList(ICollection<CallArg> value) => new CallArg { Type = CallArgType.List, Value = value };

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as CallArg is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as CallArg);
        }

        public bool Equals(CallArg? other)
        {
            return other is not null &&
                   Type == other.Type &&
                   EqualityComparer<object>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode() => HashCode.Combine(Type, Value);
        public static bool operator ==(CallArg? left, CallArg? right) => EqualityComparer<CallArg>.Default.Equals(left, right);
        public static bool operator !=(CallArg? left, CallArg? right) => !(left == right);
    }
}