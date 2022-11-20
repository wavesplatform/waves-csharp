using Google.Protobuf;

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

        public static DataTransactionBuilder Params(ICollection<EntryData> data)
        {
            return new DataTransactionBuilder(data);
        }

        public override long CalculatedFee()
        {
            var payloadSize = DataTransactionBinarySerializer.CreateDataProto(Transaction.Data).ToByteArray().Length;
            return DataTransaction.MinFee * (1 + (payloadSize - 1) / 1024);
        }
    }
}