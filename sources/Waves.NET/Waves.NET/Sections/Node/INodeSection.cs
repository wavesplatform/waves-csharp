using Waves.NET.Node.ReturnTypes;

namespace Waves.NET.Node
{
    public interface INodeSection
    {
        NodeStatus GetNodeStatus();
        string GetVersion();
    }
}