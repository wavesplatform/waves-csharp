using Google.Protobuf;
using Waves.NET.Transactions.Common;
using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions.Builders
{
    public abstract class TransactionBuilder<TB, T> where T : Transaction, new() where TB : TransactionBuilder<TB, T>
    {
        protected int Version { get; private set; }
        protected byte ChainId { get; private set; }
        protected long Fee { get; private set; }
        protected long ExtraFee { get; private set; }
        protected PublicKey SenderPublicKey { get; private set; }
        protected long Timestamp { get; private set; }
        protected ICollection<string> Proofs { get; private set; } = new LinkedList<string>();
        private PrivateKey? Signer { get; set; }

        protected T Transaction { get; private set; } = new();

        public TransactionBuilder(int defaultVersion, long defaultFee, int transactionType)
        {
            ChainId = WavesConfig.ChainId;
            Version = defaultVersion;
            Fee = defaultFee;
            ExtraFee = 0;
            SenderPublicKey = PublicKey.Zero;
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            Proofs = new List<string>();

            Transaction.Type = transactionType;
        }

        public TB AddProof(string proof)
        {
            if (string.IsNullOrWhiteSpace(proof)) return (TB)this;

            Proofs.Add(proof);

            return (TB)this;
        }

        public TB SignedWith(PrivateKey signer)
        {
            if (SenderPublicKey is null || SenderPublicKey.IsZero)
            {
                SenderPublicKey = signer.PublicKey;
            }

            Signer = signer;

            return GetUnsigned();
        }

        public TB GetUnsigned()
        {
            if (Timestamp == 0)
                Timestamp = DateTime.UtcNow.Millisecond;

            return (TB)this;
        }

        public TB SetSender(PublicKey sender)
        {
            SenderPublicKey = sender;
            return (TB)this;
        }

        public TB SetExtraFee(long extraFee)
        {
            ExtraFee = extraFee;
            return (TB)this;
        }

        public TB SetFee(long fee)
        {
            Fee = fee;
            return (TB)this;
        }

        public TB SetTimestamp(long timestamp)
        {
            Timestamp = timestamp;
            return (TB)this;
        }

        public TB SetTimestamp(int version)
        {
            Version = version;
            return (TB)this;
        }

        public byte[] ToProtobuf()
        {
            var tx = (INonGenesisTransaction)Transaction;
            var proto = new TransactionProto();
            proto.ChainId = ChainId;
            proto.SenderPublicKey = Google.Protobuf.ByteString.CopyFrom(SenderPublicKey);
            proto.Fee = new AmountProto { AssetId = Google.Protobuf.ByteString.Empty, Amount_ = tx.Fee };
            proto.Timestamp= tx.Timestamp;
            proto.Version = tx.Version;

            ToProtobuf(proto);

            return proto.ToByteArray();
        }

        protected abstract void ToProtobuf(TransactionProto proto);

        public T Build()
        {
            Transaction.Version = Version;
            Transaction.Sender = Address.FromPublicKey(ChainId, SenderPublicKey);
            Transaction.SenderPublicKey = SenderPublicKey;
            Transaction.Fee = Fee + ExtraFee;
            Transaction.Timestamp = Timestamp;
            Transaction.Proofs = Proofs;

            if (Signer is not null)
            {
                AddProof(new Base58(Signer.Sign(ToProtobuf())));
            }
            else { } //TODO!

            return Transaction;
        }
    }
}