using System.Runtime.Serialization;

namespace Waves.NET.Transactions
{
    public enum CallArgType: byte
    {
        [EnumMember(Value = "integer")]
        Integer = 0, //long
        [EnumMember(Value = "binary")]
        ByteArray = 1,
        [EnumMember(Value = "string")]
        String = 2,
        [EnumMember(Value = "boolean")]
        Boolean = 6,
        //[EnumMember(Value = "boolean")]
        //LogicalFalse = 7,
        [EnumMember(Value = "list")]
        List = 11
    }
}
/*
    0 — argument type is long.
    1 — argument type is an array of bytes.
    2 — argument type is a string.
    6 — argument type is logical True.
    7 — argument type is logical False.
    11 – argument type is list.
    Json values: (binary, boolean, integer, list, string)
*/