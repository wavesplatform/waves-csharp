namespace Waves.NET.Tests.Sections
{
    [TestClass]
    public class BlockchainNodeTest : NodeTestBase
    {
        [TestMethod]
        public void BlockchainRewardTest()
        {
            var height = Node.GetHeight();
            var rewards = Node.GetBlockchainRewards();

            Assert.AreEqual(rewards, Node.GetBlockchainRewards(rewards.Height));
            Assert.AreNotEqual(rewards, Node.GetBlockchainRewards(height - 1));

            Assert.IsTrue(rewards.Height >= height && rewards.Height <= height + 10);
            Assert.IsTrue(rewards.TotalWavesAmount > 10000000000000000);
            Assert.IsTrue(rewards.CurrentReward >= 500000000 && rewards.CurrentReward <= 700000000);
            Assert.AreEqual(50000000, rewards.MinIncrement);
            Assert.AreEqual(6, rewards.Term);
            Assert.IsTrue(rewards.NextCheck > rewards.VotingIntervalStart);
            Assert.AreEqual(3, rewards.VotingInterval);
            Assert.AreEqual(2, rewards.VotingThreshold);
            Assert.IsTrue(new[] { rewards.Votes.Increase, rewards.Votes.Decrease }.Contains(0));
        }
    }
}