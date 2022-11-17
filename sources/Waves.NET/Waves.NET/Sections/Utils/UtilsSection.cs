using Waves.NET.Transactions.Utils;
using Waves.NET.Utils.ReturnTypes;

namespace Waves.NET.Utils
{
    public class UtilsSection : SectionBase, IUtilsSection
    {
        public UtilsSection(HttpClient httpClient) : base(httpClient, "utils") { }

        public string GenerateRandomSeed()
        {
            return PublicRequest<dynamic>(HttpMethod.Get, "seed").seed;
        }

        public string GenerateRandomSeedOfLength(int length)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"seed/{length}").seed;
        }

        public NodeTime GetNodeTimeUtc()
        {
            return PublicRequest<NodeTime>(HttpMethod.Get, "time");
        }

        public string GetFastHash(string message)
        {
            return PublicRequest<dynamic>(HttpMethod.Post, "hash/fast", message).hash;
        }

        public string GetSecureHash(string message)
        {
            return PublicRequest<dynamic>(HttpMethod.Post, "hash/secure", message).hash;
        }

        public ScriptResult GetScriptCompiledCode(string script, bool compact = false)
        {
            return PublicRequest<ScriptResult>(HttpMethod.Post, "script/compileCode", script);
        }

        public ScriptResult GetScriptCompiledCodeWithImports(string scriptWithImports)
        {
            return PublicRequest<ScriptResult>(HttpMethod.Post, "script/compileWithImports", scriptWithImports);
        }

        public string GetScriptDecompiledCode(string code)
        {
            return PublicRequest<dynamic>(HttpMethod.Post, "script/decompile", code).script;
        }

        public ScriptEstimateResult GetScriptEstimate(string code)
        {
            return PublicRequest<dynamic>(HttpMethod.Post, "script/estimate", code).script;
        }

        public ScriptEvaluationResult EvaluateScript(string address, ScriptEvaluationExpression evaluationExpression)
        {
            var jsonBody = JsonUtils.Serialize(evaluationExpression);
            return PublicRequest<ScriptEvaluationResult>(HttpMethod.Post, $"script/evaluate/{address}", jsonBody);
        }
    }
}