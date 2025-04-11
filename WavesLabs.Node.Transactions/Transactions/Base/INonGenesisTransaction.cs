using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface INonGenesisTransaction : ITransaction
    {
        IRecipient Sender { get; set; }
        PublicKey SenderPublicKey { get; set; }
        ApplicationStatus ApplicationStatus { get; set; }
        int Version { get; set; }
        ICollection<Base58s> Proofs { get; set; }
        Base58s? FeeAssetId { get; set; }
    }
}
