using System.Runtime.Serialization;

namespace WavesLabs.Node.Transactions
{
    public enum LeaseStatus
    {
        [EnumMember(Value = "")]
        NotSet = 0,
        Active,
        Canceled
    }
}