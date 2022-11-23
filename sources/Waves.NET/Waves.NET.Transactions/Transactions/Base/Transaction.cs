using Newtonsoft.Json;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.JsonConverters;

namespace Waves.NET.Transactions
{
    [JsonConverter(typeof(TransactionJsonConverter))]
    public abstract class Transaction : ITransaction, INonGenesisTransaction, IEquatable<Transaction?>
    {
        public Base58s? Id { get; set; }
        public int Type { get; set; }
        public IRecipient Sender { get; set; } = null!;
        public PublicKey SenderPublicKey { get; set; } = null!;
        public long Timestamp { get; set; }
        public byte ChainId { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public int Version { get; set; }
        public long Fee { get; set; }
        public Base58s? FeeAssetId { get; set; }
        public Base58s? Signature { get; set; }
        public ICollection<Base58s> Proofs { get; set; } = new List<Base58s>();
        public int Height { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as Transaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as Transaction);
        }

        public bool Equals(Transaction? other)
        {
            return other is not null &&
                   EqualityComparer<Base58s?>.Default.Equals(Id, other.Id) &&
                   Type == other.Type &&
                   EqualityComparer<IRecipient>.Default.Equals(Sender, other.Sender) &&
                   EqualityComparer<PublicKey>.Default.Equals(SenderPublicKey, other.SenderPublicKey) &&
                   Timestamp == other.Timestamp &&
                   ChainId == other.ChainId &&
                   ApplicationStatus == other.ApplicationStatus &&
                   Version == other.Version &&
                   Fee == other.Fee &&
                   EqualityComparer<Base58s?>.Default.Equals(FeeAssetId, other.FeeAssetId) &&
                   EqualityComparer<Base58s?>.Default.Equals(Signature, other.Signature) &&
                   Height == other.Height &&
                   (Proofs is null && other.Proofs is null || Proofs.SequenceEqual(other.Proofs));
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Type);
            hash.Add(Sender);
            hash.Add(SenderPublicKey);
            hash.Add(Timestamp);
            hash.Add(ChainId);
            hash.Add(ApplicationStatus);
            hash.Add(Version);
            hash.Add(Fee);
            hash.Add(FeeAssetId);
            hash.Add(Signature);
            hash.Add(Proofs.CalcHashCode());
            hash.Add(Height);
            return hash.ToHashCode();
        }

        public static bool operator ==(Transaction? left, Transaction? right) => EqualityComparer<Transaction>.Default.Equals(left, right);
        public static bool operator !=(Transaction? left, Transaction? right) => !(left == right);
    }
}
