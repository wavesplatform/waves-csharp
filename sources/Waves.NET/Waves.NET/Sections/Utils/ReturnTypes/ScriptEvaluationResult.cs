using Waves.NET.Transactions.Common;

namespace Waves.NET.Utils
{
    public record ScriptEvaluationResult
    {
        public string Address { get; set; } = "";
        public string Expr { get; set; } = "";
        public TypeValuePair<string, string> Result { get; set; } = null!;
    }
}