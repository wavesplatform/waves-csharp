using WavesLabs.Node.Transactions.Common;

namespace WavesLabs.Node.Transactions
{
    public class MassTransferTransactionBuilder : TransactionBuilder<MassTransferTransactionBuilder, MassTransferTransaction>
    {
        public MassTransferTransactionBuilder() : base(MassTransferTransaction.LatestVersion, MassTransferTransaction.MinFee, MassTransferTransaction.TYPE) { }

        public MassTransferTransactionBuilder(ICollection<Transfer> transfers, AssetId? assetId = null, Base58s? attachment = null) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Attachment = attachment ?? Base58s.Empty;
            Transaction.Transfers = transfers;
        }

        public static MassTransferTransactionBuilder Params(ICollection<Transfer> transfers, AssetId? assetId = null, Base58s? attachment = null)
        {
            return new MassTransferTransactionBuilder(transfers, assetId, attachment);
        }

        public static MassTransferTransactionBuilder Params(Transfer transfer, AssetId? assetId = null, Base58s? attachment = null)
        {
            return new MassTransferTransactionBuilder(new[] { transfer }, assetId, attachment);
        }

        public override long CalculatedFee()
        {
            return Transaction.Transfers is null || !Transaction.Transfers.Any()
                ? MassTransferTransaction.MinFee
                : MassTransferTransaction.MinFee * (1 + (Transaction.Transfers.Count + 1) / 2);
        }

        public MassTransferTransactionBuilder SetAssetId(AssetId? assetId)
        {
            Transaction.AssetId = assetId;
            return this;
        }
    }
}