using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class TransferTransaction : Transaction, ITransferTransaction, IEquatable<TransferTransaction?>
    {
        public const int TYPE = 4;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public IRecipient Recipient { get; set; } = null!;
        public Base58s? AssetId { get; set; }
        public Base58s? FeeAsset { get; set; }
        public long Amount { get; set; }
        public string Attachment { get; set; } = null!;

        public TransferTransaction() => Type = TYPE;

        public override bool Equals(object? obj)
        {
            if (obj is null || obj as TransferTransaction is null) return false;
            return ReferenceEquals(this, obj) || Equals(obj as TransferTransaction);
        }

        public bool Equals(TransferTransaction? other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<IRecipient>.Default.Equals(Recipient, other.Recipient) &&
                   EqualityComparer<Base58s?>.Default.Equals(AssetId, other.AssetId) &&
                   EqualityComparer<Base58s?>.Default.Equals(FeeAsset, other.FeeAsset) &&
                   Amount == other.Amount &&
                   Attachment == other.Attachment;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Recipient, AssetId, FeeAsset, Amount, Attachment);
        public static bool operator ==(TransferTransaction? left, TransferTransaction? right) => EqualityComparer<TransferTransaction>.Default.Equals(left, right);
        public static bool operator !=(TransferTransaction? left, TransferTransaction? right) => !(left == right);
    }
}