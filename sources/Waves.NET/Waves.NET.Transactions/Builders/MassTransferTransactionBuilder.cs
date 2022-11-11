namespace Waves.NET.Transactions.Builders
{
    public class MassTransferTransactionBuilder : TransactionBuilder<MassTransferTransactionBuilder, MassTransferTransaction>
    {
        public MassTransferTransactionBuilder() : base(MassTransferTransaction.LatestVersion, MassTransferTransaction.MinFee, MassTransferTransaction.TYPE) { }

        public MassTransferTransactionBuilder(string assetId, string attachment, int transferCount, long totalAmount, ICollection<Transfer> transfer) : this()
        {
            Transaction.AssetId = assetId;
            Transaction.Attachment = attachment;
            Transaction.TransferCount = transferCount;
            Transaction.TotalAmount = totalAmount;
            Transaction.Transfers = transfer;
        }

        public static MassTransferTransactionBuilder Data(string assetId, string attachment, int transferCount, long totalAmount, ICollection<Transfer> transfer)
        {
            return new MassTransferTransactionBuilder(assetId, attachment, transferCount, totalAmount, transfer);
        }
    }
}