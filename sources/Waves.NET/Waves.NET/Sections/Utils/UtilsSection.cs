﻿using Waves.NET.Transactions.Utils;
using Waves.NET.Utils.ReturnTypes;

namespace Waves.NET.Utils
{
    public class UtilsSection : SectionBase, IUtilsSection
    {
        public UtilsSection(HttpClient httpClient) : base(httpClient, "utils") { }

        /// <summary>
        /// Generate random seed. The returned value is base58 encoded
        /// </summary>
        /// <returns>Base58 encoded seed</returns>
        public string GenerateRandomSeed()
        {
            return PublicRequest<dynamic>(HttpMethod.Get, "seed").seed;
        }

        /// <summary>
        /// Generate random seed of a given length in bytes. The returned value is base58 encoded
        /// </summary>
        /// <param name="length">Seed length</param>
        /// <returns>base58 result string</returns>
        public string GenerateRandomSeedOfLength(int length)
        {
            return PublicRequest<dynamic>(HttpMethod.Get, $"seed/{length}").seed;
        }

        /// <summary>
        /// Get Current Node time (UTC)
        /// </summary>
        /// <returns>Time</returns>
        public NodeTime GetNodeTimeUtc()
        {
            return PublicRequest<NodeTime>(HttpMethod.Get, "time");
        }

        /// <summary>
        /// Calculate the BLAKE2b-256 hash of a given message
        /// </summary>
        /// <param name="message">Message to hash</param>
        /// <returns>Hash string</returns>
        public string GetFastHash(string message)
        {
            return PublicRequest<dynamic>(HttpMethod.Post, "hash/fast", message).hash;
        }

        /// <summary>
        /// Calculate the <see href="https://keccak.team/files/Keccak-submission-3.pdf">Keccak-256</see>
        /// hash of the <see href="https://en.wikipedia.org/wiki/BLAKE_%28hash_function%29">BLAKE2b-256</see> hash of a given message
        /// </summary>
        /// <param name="message">Message to hash</param>
        /// <returns>Hash string</returns>
        public string GetSecureHash(string message)
        {
            return PublicRequest<dynamic>(HttpMethod.Post, "hash/secure", message).hash;
        }

        /// <summary>
        /// Compiles string code to base64 script representation
        /// </summary>
        /// <param name="script">Code</param>
        /// <param name="compact">If true, compacts the contract. False by default</param>
        /// <returns>Compiled script</returns>
        public ScriptResult GetScriptCompiledCode(string script, bool compact = false)
        {
            return PublicRequest<ScriptResult>(HttpMethod.Post, "script/compileCode", script);
        }

        /// <summary>
        /// Compiles string code with imports to base64 script representation
        /// </summary>
        /// <param name="scriptWithImports">
        /// Json string like:
        /// <code>{
        ///    "script": "string",
        ///    "imports": {
        ///        "additionalProp1": "string",
        ///        "additionalProp2": "string",
        ///        "additionalProp3": "string"
        ///    }
        ///}</code></param>
        /// <returns>Compiled script</returns>
        public ScriptResult GetScriptCompiledCodeWithImports(string scriptWithImports)
        {
            return PublicRequest<ScriptResult>(HttpMethod.Post, "script/compileWithImports", scriptWithImports);
        }

        /// <summary>
        /// Decompiles base64 script representation to string code
        /// </summary>
        /// <param name="code">base64 string of script code</param>
        /// <returns>Decompiled script code</returns>
        public string GetScriptDecompiledCode(string code)
        {
            return PublicRequest<dynamic>(HttpMethod.Post, "script/decompile", code).script;
        }

        /// <summary>
        /// Estimates complexity of a given compiled code
        /// </summary>
        /// <param name="code">Compiled code in base64 script representation</param>
        /// <returns></returns>
        public ScriptEstimateResult GetScriptEstimate(string code)
        {
            return PublicRequest<dynamic>(HttpMethod.Post, "script/estimate", code).script;
        }

        /// <summary>
        /// Evaluates the provided expression, taking into account the deployed dApp contract
        /// </summary>
        /// <param name="address"></param>
        /// <param name="evaluationExpression">Expression to evaluate</param>
        /// <returns>Evaluation result</returns>
        public ScriptEvaluationResult EvaluateScript(string address, ScriptEvaluationExpression evaluationExpression)
        {
            var jsonBody = JsonUtils.Serialize(evaluationExpression);
            return PublicRequest<ScriptEvaluationResult>(HttpMethod.Post, $"script/evaluate/{address}", jsonBody);
        }
    }
}