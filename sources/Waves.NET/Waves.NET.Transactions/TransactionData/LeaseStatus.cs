using System.Runtime.Serialization;

namespace Waves.NET.Transactions
{
    public enum LeaseStatus
    {
        [EnumMember(Value = "")]
        NotSet = 0,
        Active,
        Canceled
    }
}