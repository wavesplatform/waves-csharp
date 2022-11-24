using Waves.NET.ReturnTypes;

namespace Waves.NET.Sections
{
    public interface IUtilsSection
    {
        /// <summary>
        /// Generate random seed. The returned value is base58 encoded
        /// </summary>
        /// <returns>Base58 encoded seed</returns>
        public string GenerateRandomSeed();

        /// <summary>
        /// Generate random seed of a given length in bytes. The returned value is base58 encoded
        /// </summary>
        /// <param name="length">Seed length</param>
        /// <returns>base58 result string</returns>
        public string GenerateRandomSeedOfLength(int length);

        /// <summary>
        /// Get Current Node time (UTC)
        /// </summary>
        /// <returns>Time</returns>
        public NodeTime GetNodeTimeUtc();

        /// <summary>
        /// Calculate the BLAKE2b-256 hash of a given message
        /// </summary>
        /// <param name="message">Message to hash</param>
        /// <returns>Hash string</returns>
        public string GetFastHash(string message);

        /// <summary>
        /// Calculate the <see href="https://keccak.team/files/Keccak-submission-3.pdf">Keccak-256</see>
        /// hash of the <see href="https://en.wikipedia.org/wiki/BLAKE_%28hash_function%29">BLAKE2b-256</see> hash of a given message
        /// </summary>
        /// <param name="message">Message to hash</param>
        /// <returns>Hash string</returns>
        public string GetSecureHash(string message);

        /// <summary>
        /// Compiles string code to base64 script representation
        /// </summary>
        /// <param name="script">Code</param>
        /// <param name="compact">If true, compacts the contract. False by default</param>
        /// <returns>Compiled script</returns>
        public ScriptInfo CompileScript(string script, bool compact = false);

        /// <summary>
        /// Decompiles base64 script representation to string code
        /// </summary>
        /// <param name="code">base64 string of script code</param>
        /// <returns>Decompiled script code</returns>
        public string DecompileScript(string code);
    }
}