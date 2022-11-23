using Waves.NET.ReturnTypes;
using Waves.NET.Transactions.Utils;

namespace Waves.NET.Sections
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

        public ScriptInfo CompileScript(string script, bool compact = false)
        {
            var url = "script/compileCode";
            return PublicRequest<ScriptInfo>(HttpMethod.Post, compact ? url += "?compact=true" : url, script);
        }

        public ScriptInfo GetScriptCompiledCodeWithImports(string scriptWithImports)
        {
            return PublicRequest<ScriptInfo>(HttpMethod.Post, "script/compileWithImports", scriptWithImports);
        }

        public string DecompileScript(string code)
        {
            return PublicRequest<dynamic>(HttpMethod.Post, "script/decompile", code).script;
        }

        public ScriptInfo GetScriptEstimate(string code)
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