namespace WavesLabs.Node.Client.ReturnTypes
{
    public record BlockchainRewardVotes
    {
        public int Increase { get; init; }
        public int Decrease { get; init; }
    }
}
