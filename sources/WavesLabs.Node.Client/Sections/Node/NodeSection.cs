using WavesLabs.Node.Client.ReturnTypes;

namespace WavesLabs.Node.Client.Sections
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