﻿using System.Runtime.Serialization;

namespace Waves.NET.Transactions
{
    public enum ApplicationStatus
    {
        [EnumMember(Value = "")]
        NotSet = 0,

        Succeeded,

        [EnumMember(Value = "script_execution_failed")]
        ScriptExecutionFailed
    }
}