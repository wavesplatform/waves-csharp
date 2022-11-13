using Waves.NET.Node.ReturnTypes;

namespace Waves.NET.Node
{
    public class NodeSection : SectionBase, INodeSection
    {
        public NodeSection(HttpClient httpClient) : base(httpClient, "node") { }

        /// <summary>
        /// Get Waves node version
        /// </summary>
        /// <returns>Node version</returns>
        public string GetVersion()
        {
            return PublicRequest<dynamic>(HttpMethod.Get, "version").version;
        }

        /// <summary>
        /// Get status of the running core
        /// </summary>
        /// <returns>Node status</returns>
        public NodeStatus GetNodeStatus()
        {
            return PublicRequest<NodeStatus>(HttpMethod.Get, "status");
        }
    }
}