namespace Waves.NET.Transactions
{
    public record TransactionMerkleProofs
    {
        public string Id { get; init; } = null!;
        public int TransactionIndex { get; init; }
        public ICollection<string> MerkleProof { get; init; } = null!;
    }
}