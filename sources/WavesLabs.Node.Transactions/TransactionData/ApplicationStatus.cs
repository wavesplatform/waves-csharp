using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WavesLabs.Node.Transactions
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ApplicationStatus
    {
        [EnumMember(Value = "")]
        NotSet = 0,

        [EnumMember(Value = "succeeded")]
        Succeeded,

        [EnumMember(Value = "script_execution_failed")]
        ScriptExecutionFailed,

        [EnumMember(Value = "script_execution_in_progress")]
        ScriptExecutionInProgress
    }
}