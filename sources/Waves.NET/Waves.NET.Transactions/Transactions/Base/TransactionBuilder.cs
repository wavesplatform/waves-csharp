using Waves.NET.Transactions.Common;

namespace Waves.NET.Transactions
{
    public abstract class TransactionBuilder<TB, T> where T : Transaction, new() where TB : TransactionBuilder<TB, T>
    {
        private TransactionBinarySerializerFactory _transactionBinarySerializerFactory = new TransactionBinarySerializerFactory();

        protected int Version { get; private set; }
        protected byte ChainId { get; private set; }
        protected long Fee { get; private set; }
        protected long ExtraFee { get; private set; }
        protected PublicKey SenderPublicKey { get; private set; }
        protected long Timestamp { get; private set; }
        protected ICollection<Base58s> Proofs { get; private set; } = new LinkedList<Base58s>();

        protected T Transaction { get; private set; } = new();

        public TransactionBuilder(int defaultVersion, long defaultFee, int transactionType)
        {
            ChainId = WavesConfig.ChainId;
            Version = defaultVersion;
            Fee = defaultFee;
            ExtraFee = 0;
            SenderPublicKey = PublicKey.Zero;
            Timestamp = 0;
            Proofs = new List<Base58s>();

            Transaction.Type = transactionType;
            Transaction.Version = transactionType;
        }

        public TB AddProof(Base58s proof)
        {
            if (string.IsNullOrWhiteSpace(proof)) return (TB)this;

            Proofs.Add(proof);

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

        public TB SetVersion(int version)
        {
            Version = version;
            return (TB)this;
        }

        public T GetSignedWith(PrivateKey signer)
        {
            if (SenderPublicKey is null || SenderPublicKey.IsZero)
            {
                SenderPublicKey = signer.PublicKey;
            }

            var tx = GetUnsigned();
            var bodyBytes = _transactionBinarySerializerFactory.GetFor(tx).Serialize(tx);
            tx.Proofs.Add(new Base58s(signer.Sign(bodyBytes)));

            return tx;
        }

        public T GetUnsigned()
        {
            if (Timestamp == 0)
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            Transaction.Sender = Address.FromPublicKey(ChainId, SenderPublicKey);
            Transaction.SenderPublicKey = SenderPublicKey;
            Transaction.Fee = Fee + ExtraFee;
            Transaction.Timestamp = Timestamp;
            Transaction.Proofs = Proofs;
            Transaction.Version = Version;
            Transaction.ChainId = ChainId;

            return Transaction;
        }
    }
}