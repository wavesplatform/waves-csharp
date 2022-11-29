using Google.Protobuf;
using Waves;

namespace WavesLabs.Node.Transactions
{
    public class DataTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 2 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IDataTransaction)transaction;
            proto.DataTransaction = CreateDataProto(tx.Data);
        }

        public static DataTransactionData CreateDataProto(ICollection<EntryData> data)
        {
            var proto = new DataTransactionData();
            proto.Data.Add(data.Select(x =>
            {
                switch (x)
                {
                    case BooleanEntry be: return new DataTransactionData.Types.DataEntry { BoolValue = be.Value, Key = be.Key };
                    case IntegerEntry ie: return new DataTransactionData.Types.DataEntry { IntValue = ie.Value, Key = ie.Key };
                    case StringEntry se: return new DataTransactionData.Types.DataEntry { StringValue = se.Value, Key = se.Key };
                    case BinaryEntry biv: return new DataTransactionData.Types.DataEntry { BinaryValue = ByteString.FromBase64(biv.Value), Key = biv.Key };
                    case DeleteEntry de: return new DataTransactionData.Types.DataEntry { Key = de.Key };
                    default: throw new ArgumentException($"Unknown data entry type: {x.GetType()}");
                }
            }));
            return proto;
        }
    }
}