using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public interface IEthereumTransaction
    {
        string Bytes { get; set; }
        EthTransactionPayload Payload { get; set; }

        int Type { get; set; }
        int Version { get; set; }
        int Height { get; set; }
        long Fee { get; set; }
        long Timestamp { get; set; }
        byte ChainId { get; set; }
        Base58s? Id { get; set; }
        Base58s? FeeAssetId { get; set; }
        PublicKey SenderPublicKey { get; set; }
        IRecipient Sender { get; set; }
        ApplicationStatus ApplicationStatus { get; set; }
    }
}