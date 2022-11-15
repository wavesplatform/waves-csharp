namespace Waves.NET.Transactions
{
    public class DataTransactionBuilder : TransactionBuilder<DataTransactionBuilder, DataTransaction>
    {
        public DataTransactionBuilder() :
            base(DataTransaction.LatestVersion, DataTransaction.MinFee, DataTransaction.TYPE)
        { }

        public DataTransactionBuilder(ICollection<EntryData> data) : this()
        {
            Transaction.Data = data;
        }

        public static DataTransactionBuilder Data(ICollection<EntryData> data)
        {
            return new DataTransactionBuilder(data);
        }
    }
}