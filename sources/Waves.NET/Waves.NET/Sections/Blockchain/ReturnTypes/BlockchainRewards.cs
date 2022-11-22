namespace Waves.NET.ReturnTypes
{
    public record BlockchainRewards
    {
        public int Height { get; init; }
        public long TotalWavesAmount { get; init; }
        public long CurrentReward { get; init; }
        public long MinIncrement { get; init; }
        public int Term { get; init; }
        public int NextCheck { get; init; }
        public int VotingIntervalStart { get; init; }
        public int VotingInterval { get; init; }
        public int VotingThreshold { get; init; }
        public BlockchainRewardVotes Votes { get; init; } = null!;
    }
}
