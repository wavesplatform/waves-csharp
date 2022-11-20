using Newtonsoft.Json;

namespace Waves.NET.Blocks.ReturnTypes
{
    public class BlockHeader : IEquatable<BlockHeader?>
    {
        public long Timestamp { get; init; }
        public int Version { get; init; }
        public int Height { get; init; }
        public long TotalFee { get; init; }
        public string Reference { get; init; } = null!;
        public string Generator { get; init; } = null!;
        public string GeneratorPublicKey { get; init; } = null!;
        public string Signature { get; init; } = null!;
        public string Id { get; init; } = null!;
        public int Blocksize { get; init; }
        public int TransactionCount { get; init; }
        public long Reward { get; init; }
        public long DesiredReward { get; init; }
        public string? VRF { get; init; } = null!;
        public string? TransactionsRoot { get; init; } = null!;
        public int TransactionsCount { get; init; }
        [JsonProperty("nxt-consensus")]
        public NxtConsensus NxtConsensus { get; init; } = null!;
        public ICollection<int>? Features { get; init; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as BlockHeader is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as BlockHeader);
        }

        public bool Equals(BlockHeader? other)
        {
            return other is not null &&
                Timestamp == other.Timestamp &&
                Version == other.Version &&
                Height == other.Height &&
                TotalFee == other.TotalFee &&
                Reference == other.Reference &&
                Generator == other.Generator &&
                GeneratorPublicKey == other.GeneratorPublicKey &&
                Signature == other.Signature &&
                Id == other.Id &&
                Blocksize == other.Blocksize &&
                TransactionCount == other.TransactionCount &&
                Reward == other.Reward &&
                DesiredReward == other.DesiredReward &&
                VRF == other.VRF &&
                TransactionsRoot == other.TransactionsRoot &&
                TransactionsCount == other.TransactionsCount &&
                NxtConsensus == other.NxtConsensus &&
                (Features is null && other.Features is null || Features.SequenceEqual(other.Features));
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Timestamp);
            hash.Add(Version);
            hash.Add(Height);
            hash.Add(TotalFee);
            hash.Add(Reference);
            hash.Add(Generator);
            hash.Add(GeneratorPublicKey);
            hash.Add(Signature);
            hash.Add(Id);
            hash.Add(Blocksize);
            hash.Add(TransactionCount);
            hash.Add(Reward);
            hash.Add(DesiredReward);
            hash.Add(VRF);
            hash.Add(TransactionsRoot);
            hash.Add(TransactionsCount);
            hash.Add(NxtConsensus);
            hash.Add(Features);
            return hash.ToHashCode();
        }

        public static bool operator ==(BlockHeader? left, BlockHeader? right) => EqualityComparer<BlockHeader>.Default.Equals(left, right);
        public static bool operator !=(BlockHeader? left, BlockHeader? right) => !(left == right);
    }
}