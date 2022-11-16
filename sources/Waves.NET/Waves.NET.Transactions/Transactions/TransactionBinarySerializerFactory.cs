using Waves.NET.Transactions.TransactionData;

namespace Waves.NET.Transactions
{
    public sealed class TransactionBinarySerializerFactory
    {
        private readonly Dictionary<TransactionType, Lazy<ITransactionBinarySerializer>> _serializers;

        public ITransactionBinarySerializer GetFor(Transaction transaction) => Get((TransactionType)transaction.Type);

        public ITransactionBinarySerializer Get(TransactionType transactionType)
        {
            var lazy = _serializers.GetValueOrDefault(transactionType);
            if (lazy == null) throw new NotSupportedException($"Transaction type {transactionType} is not supported.");
            return lazy.Value;
        }

        public TransactionBinarySerializerFactory()
        {
            _serializers = new Dictionary<TransactionType, Lazy<ITransactionBinarySerializer>>();
            _serializers.Add(TransactionType.Genesis, new Lazy<ITransactionBinarySerializer>(() => new GenesisTransactionBinarySerializer()));
            _serializers.Add(TransactionType.Issue, new Lazy<ITransactionBinarySerializer>(() => new IssueTransactionBinarySerializer()));
            _serializers.Add(TransactionType.Transfer, new Lazy<ITransactionBinarySerializer>(() => new TransferTransactionBinarySerializer()));
            _serializers.Add(TransactionType.Reissue, new Lazy<ITransactionBinarySerializer>(() => new ReissueTransactionBinarySerializer()));
            _serializers.Add(TransactionType.Burn, new Lazy<ITransactionBinarySerializer>(() => new BurnTransactionBinarySerializer()));
            _serializers.Add(TransactionType.Exchange, new Lazy<ITransactionBinarySerializer>(() => new ExchangeTransactionBinarySerializer()));
            _serializers.Add(TransactionType.Lease, new Lazy<ITransactionBinarySerializer>(() => new LeaseTransactionBinarySerializer()));
            _serializers.Add(TransactionType.LeaseCancel, new Lazy<ITransactionBinarySerializer>(() => new LeaseCancelTransactionBinarySerializer()));
            _serializers.Add(TransactionType.CreateAlias, new Lazy<ITransactionBinarySerializer>(() => new CreateAliasTransactionBinarySerializer()));
            _serializers.Add(TransactionType.MassTransfer, new Lazy<ITransactionBinarySerializer>(() => new MassTransferTransactionBinarySerializer()));
            _serializers.Add(TransactionType.Data, new Lazy<ITransactionBinarySerializer>(() => new DataTransactionBinarySerializer()));
            _serializers.Add(TransactionType.SetScript, new Lazy<ITransactionBinarySerializer>(() => new SetScriptTransactionBinarySerializer()));
            _serializers.Add(TransactionType.SponsorFee, new Lazy<ITransactionBinarySerializer>(() => new SponsorFeeTransactionBinarySerializer()));
            _serializers.Add(TransactionType.SetAssetScript, new Lazy<ITransactionBinarySerializer>(() => new SetAssetScriptTransactionBinarySerializer()));
            _serializers.Add(TransactionType.InvokeScript, new Lazy<ITransactionBinarySerializer>(() => new InvokeScriptTransactionBinarySerializer()));
            _serializers.Add(TransactionType.UpdateAssetInfo, new Lazy<ITransactionBinarySerializer>(() => new UpdateAssetInfoTransactionBinarySerializer()));
        }
    }
}
