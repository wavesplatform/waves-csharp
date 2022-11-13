using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions
{
    public class TransferTransaction : Transaction, ITransferTransaction
    {
        public const int TYPE = 4;
        public const int LatestVersion = 3;
        public const int MinFee = 100000;

        public IRecipient Recipient { get; set; } = null!;
        public string? AssetId { get; set; }
        public string? FeeAsset { get; set; }
        public long Amount { get; set; }
        public string Attachment { get; set; } = null!;
    }

    public interface ITransferTransaction : INonGenesisTransaction
    {
        IRecipient Recipient { get; set; }
        string? AssetId { get; set; }
        string? FeeAsset { get; set; }
        long Amount { get; set; }
        string Attachment { get; set; }
    }
}

/*Json Example: {
    "type": 4,
    "id": "2UMEGNXwiRzyGykG8voDgxnwHA7w5aX5gmxdcf9DZZjL",
    "fee": 100000,
    "feeAssetId": null,
    "timestamp": 1583160322998,
    "version": 2,
    "sender": "3PCeQD3nAyHmzDSYBUnSPDWf9qxqzVU2sjh",
    "senderPublicKey": "6kn1XPDh2XUjVAgznxNousHq3EnKKLx7BRWyJzVFU76J",
    "proofs": [
        "2z5fnoigbsCBqRPWqTDeDmGJF6qJwnm2WLspen6c6qziTc73sBh9Kh81kPhUT9DGg7ANwqsXMxQauEvyw3RxNH7z"
    ],
    "recipient": "3P45uRnyVygTnbEJNxc2CHLUiC4izQxbuuS",
    "assetId": "51LxAtwBXapvvTFSbbh4nLyWFxH6x8ocfNvrXxbTChze",
    "feeAsset": null,
    "amount": 30077000000,
    "attachment": "2d6RhvQATwGbyv7dKT3L77758iJx",
    "height": 1954598,
    "spentComplexity": 0
}*/