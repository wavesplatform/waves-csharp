using System.Runtime.Serialization;

namespace Waves.NET.Transactions
{
    public enum EthTransactionPayloadType
    {
        [EnumMember(Value = "transfer")]
        Transfer,
        [EnumMember(Value = "invocation")]
        Invoke
    }
}