﻿using Google.Protobuf;

namespace Waves.NET.Transactions
{
    public class DataTransactionBinarySerializer : TransactionBinarySerializer
    {
        protected override IList<int> SupportedVersions => new List<int> { 2 };

        protected override void SerializeInner(TransactionProto proto, Transaction transaction)
        {
            var tx = (IDataTransaction)transaction;
            proto.DataTransaction = new DataTransactionData();

            var mappedData = tx.Data.Select(x =>
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
            });

            proto.DataTransaction.Data.Add(mappedData);
        }
    }
}