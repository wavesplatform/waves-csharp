using Waves.NET.Transactions.Crypto;

namespace Waves.NET.Transactions.Builders
{
    public abstract class TransactionBuilder<TB, T> where T : Transaction, new() where TB : TransactionBuilder<TB,T>
    {
        protected int Version { get; private set; }
        protected byte ChainId { get; private set; }
        protected long Fee { get; private set; }
        protected long ExtraFee { get; private set; }
        protected PublicKey Sender { get; private set; }
        protected long Timestamp { get; private set; }
        protected ICollection<string> Proofs { get; private set; } = new LinkedList<string>();
        private PrivateKey Signer { get; set; }

        protected T Transaction { get; private set; } = new();

        public TransactionBuilder(int defaultVersion, long defaultFee, int transactionType)
        {
            ChainId = WavesConfig.CurrentChainId;
            Version = defaultVersion;
            Fee = defaultFee;
            ExtraFee = 0;
            Sender = PublicKey.Zero;
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
            if (Sender is null || Sender.IsZero)
                Sender = signer.PublicKey;

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
            Sender = sender;
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

        //TODO: implement signing
        public T Build()
        {
            Transaction.Version = Version;
            Transaction.Sender = Sender.GetAddress(ChainId);
            Transaction.SenderPublicKey = Sender;
            Transaction.Fee = Fee + ExtraFee;
            Transaction.Timestamp = Timestamp;
            Transaction.Proofs = Proofs;

            return Transaction;
        }
    }
}