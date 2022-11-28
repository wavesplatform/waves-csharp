namespace Waves.NET.Tests.FT
{
    [TestClass]
    public class NodeNodeTests : NodeTestBase
    {
        [TestMethod]
        public void GetNodeStatusTest()
        {
            try
            {
                var status = Node.GetNodeStatus();
                Assert.IsNotNull(status);
            }
            catch(Exception ex)
            {
                Assert.Fail($"GetNodeStatusTest: {ex}");
            }
        }

        [TestMethod]
        public void GetVersionTest()
        {
            try
            {
                var version = Node.GetVersion();
                Assert.IsFalse(string.IsNullOrWhiteSpace(version));
            }
            catch (Exception ex)
            {
                Assert.Fail($"GetNodeVersionTest: {ex}");
            }
        }
    }
}