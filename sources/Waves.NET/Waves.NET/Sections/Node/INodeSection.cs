using Waves.NET.ReturnTypes;

namespace Waves.NET.Sections
{
    public interface INodeSection
    {
        /// <summary>
        /// Get status of the running core
        /// </summary>
        /// <returns>Node status</returns>
        NodeStatus GetNodeStatus();

        /// <summary>
        /// Get Waves node version
        /// </summary>
        /// <returns>Node version</returns>
        string GetVersion();
    }
}