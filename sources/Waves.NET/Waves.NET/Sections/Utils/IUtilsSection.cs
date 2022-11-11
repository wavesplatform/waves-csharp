using Waves.NET.Utils.ReturnTypes;

namespace Waves.NET.Utils
{
    public interface IUtilsSection
    {
        ScriptEvaluationResult EvaluateScript(string address, ScriptEvaluationExpression evaluationExpression);
        string GenerateRandomSeed();
        string GetFastHash(string message);
        NodeTime GetNodeTimeUtc();
        ScriptResult GetScriptCompiledCode(string script, bool compact = false);
        ScriptResult GetScriptCompiledCodeWithImports(string scriptWithImports);
        string GetScriptDecompiledCode(string code);
        ScriptEstimateResult GetScriptEstimate(string code);
        string GetSecureHash(string message);
        string SerializeObject(object obj);
    }
}