using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface IOrder
    {
        int Version { get; set; }
        byte ChainId { get; set; }
        Base58s? Id { get; set; }
        IRecipient Sender { get; set; }
        PublicKey SenderPublicKey { get; set; }
        PublicKey? MatcherPublicKey { get; set; }
        AssetPair AssetPair { get; set; }
        OrderType OrderType { get; set; }
        long Amount { get; set; }
        long Price { get; set; }
        long Timestamp { get; set; }
        long Expiration { get; set; }
        long MatcherFee { get; set; }
        Base58s? MatcherFeeAssetId { get; set; }
        ICollection<Base58s> Proofs { get; set; }
        byte[]? Eip712Signature { get; set; }
    }
}
