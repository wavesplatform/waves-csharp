using System.Runtime.Serialization;

namespace Waves.NET.Transactions
{
    public enum OrderType
    {
        [EnumMember(Value = "")]
        NotSet = 0,
        Buy,
        Sell
    }
}