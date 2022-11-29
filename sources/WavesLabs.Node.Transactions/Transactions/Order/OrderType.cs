using System.Runtime.Serialization;

namespace WavesLabs.Node.Transactions
{
    public enum OrderType
    {
        [EnumMember(Value = "")]
        NotSet = 0,
        Buy,
        Sell
    }
}