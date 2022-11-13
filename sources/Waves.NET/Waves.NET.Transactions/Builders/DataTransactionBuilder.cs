using Google.Protobuf;

namespace Waves.NET.Transactions.Builders
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

        protected override void ToProtobuf(TransactionProto proto)
        {
            var tx = (IDataTransaction)Transaction;
            proto.DataTransaction = new DataTransactionData();

            var mappedData = tx.Data.Select(x =>
            {
                switch (x)
                {
                    case BooleanEntry be: return new DataTransactionData.Types.DataEntry { BoolValue = be.Value, Key = be.Key };
                    case IntegerEntry ie: return new DataTransactionData.Types.DataEntry { IntValue = ie.Value, Key = ie.Key };
                    case StringEntry se: return new DataTransactionData.Types.DataEntry { StringValue = se.Value, Key = se.Key };
                    case BinaryEntry biv: return new DataTransactionData.Types.DataEntry { BinaryValue = ByteString.FromBase64(biv.Value), Key = biv.Key };
                    default: throw new ArgumentException($"Unknown entry type: {x.GetType()}");
                }
            });

            proto.DataTransaction.Data.Add(mappedData);
        }
    }
}