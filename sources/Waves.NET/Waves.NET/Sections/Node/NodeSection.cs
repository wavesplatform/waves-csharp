using Waves.NET.ReturnTypes;

namespace Waves.NET.Sections
{
    public class NodeSection : SectionBase, INodeSection
    {
        public NodeSection(HttpClient httpClient) : base(httpClient, "node") { }

        public string GetVersion()
        {
            return PublicRequest<dynamic>(HttpMethod.Get, "version").version;
        }

        public NodeStatus GetNodeStatus()
        {
            return PublicRequest<NodeStatus>(HttpMethod.Get, "status");
        }
    }
}