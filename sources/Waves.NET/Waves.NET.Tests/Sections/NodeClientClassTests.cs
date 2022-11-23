using Waves.NET.Transactions;

namespace Waves.NET.Tests.Sections
{
    [TestClass]
    public class NodeClientClassTests : NodeTestBase
    {
        [TestMethod]
        public void NodeClientCreationTest()
        {
            var node = NodeClient.Create(Profile.Private);
            Assert.IsNotNull(node);
            Assert.IsTrue(node is INode);
            Assert.IsTrue(node.ChainId > 0);
            Assert.AreEqual(WavesConfig.ChainId, node.ChainId);
        }

        [TestMethod]
        public void NodeClientCreationSectionedTest()
        {
            var node = NodeClient.CreateSections(Profile.Private);
            Assert.IsNotNull(node);
            Assert.IsTrue(node is INodeSections);
            Assert.IsTrue(node.ChainId > 0);
            Assert.AreEqual(WavesConfig.ChainId, node.ChainId);
            Assert.IsNotNull(node.Addresses);
            Assert.IsNotNull(node.Aliases);
            Assert.IsNotNull(node.Assets);
            Assert.IsNotNull(node.Blockchain);
            Assert.IsNotNull(node.Blocks);
            Assert.IsNotNull(node.Debug);
            Assert.IsNotNull(node.Node);
            Assert.IsNotNull(node.Transactions);
            Assert.IsNotNull(node.Utils);
            Assert.IsNotNull(node.Leasing);
        }
    }
}