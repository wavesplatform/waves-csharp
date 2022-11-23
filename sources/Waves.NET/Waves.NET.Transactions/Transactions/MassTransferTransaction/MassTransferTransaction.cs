using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public class MassTransferTransaction : Transaction, IMassTransferTransaction, IEquatable<MassTransferTransaction?>
    {
        public const int TYPE = 11;
        public const int LatestVersion = 2;
        public const int MinFee = 100000;

        public Base58s? AssetId { get; set; } = null!;
        public Base58s Attachment { get; set; } = null!;
        public int TransferCount { get; set; }
        public long TotalAmount { get; set; }
        public ICollection<Transfer> Transfers { get; set; } = null!;

        public MassTransferTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as MassTransferTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as MassTransferTransaction);
        }

        public bool Equals(MassTransferTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<Base58s?>.Default.Equals(AssetId, other.AssetId) &&
                   EqualityComparer<Base58s>.Default.Equals(Attachment, other.Attachment) &&
                   TransferCount == other.TransferCount &&
                   TotalAmount == other.TotalAmount &&
                   (Transfers is null && other.Transfers is null || Transfers.SequenceEqual(other.Transfers));
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), AssetId, Attachment, TransferCount, TotalAmount, Transfers.CalcHashCode());
        public static bool operator ==(MassTransferTransaction? left, MassTransferTransaction? right) => EqualityComparer<MassTransferTransaction>.Default.Equals(left, right);
        public static bool operator !=(MassTransferTransaction? left, MassTransferTransaction? right) => !(left == right);
    }
}