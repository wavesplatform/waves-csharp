namespace Waves.NET.Blockchain
{
    public record BlockchainRewardVotes
    {
        public int Increase { get; init; }
        public int Decrease { get; init; }
    }
}
