using Newtonsoft.Json;
using Waves.NET.Transactions.Crypto;
using Waves.NET.Transactions.JsonConverters;

namespace Waves.NET.Transactions
{
    [JsonConverter(typeof(TransactionJsonConverter))]
    public abstract class Transaction : ITransaction
    {
        public long Timestamp { get; set; }
        public byte ChainId { get; set; }
        public string Sender { get; set; } = "";
        public PublicKey SenderPublicKey { get; set; } = null!;
        public string? Id { get; set; }
        public string ApplicationStatus { get; set; } = "";
        public int Version { get; set; }
        public int Type { get; set; }
        public ICollection<string> Proofs { get; set; } = new List<string>();
        public string? Signature { get; set; }
        public long Fee { get; set; }
        public string? FeeAssetId { get; set; }
    }

    public interface ITransaction
    {
        int Type { get; set; }
        string? Id { get; set; }
        long Timestamp { get; set; }
        long Fee { get; set; }
        string? Signature { get; set; }
    }

    public interface INonGenesisTransaction : ITransaction
    {
        string Sender { get; set; }
        PublicKey SenderPublicKey { get; set; }
        string ApplicationStatus { get; set; }
        int Version { get; set; }
        ICollection<string> Proofs { get; set; }
        string? FeeAssetId { get; set; }
    }

    public interface IOrder : INonGenesisTransaction
    {
        long Amount { get; set; }
        AssetPair AssetPair { get; set; }
        long Expiration { get; set; }
        long MatcherFee { get; set; }
        string MatcherFeeAssetId { get; set; }
        PublicKey MatcherPublicKey { get; set; }
        OrderType OrderType { get; set; }
        long Price { get; set; }
    }
}
