using Newtonsoft.Json;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.JsonConverters;

namespace Waves.NET.Transactions
{
    [JsonConverter(typeof(TransactionJsonConverter))]
    public abstract class Transaction : ITransaction, INonGenesisTransaction
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
    }
}
