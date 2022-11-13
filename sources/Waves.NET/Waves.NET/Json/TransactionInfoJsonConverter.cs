﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Waves.NET.Transactions;
using Waves.NET.Transactions.Info;

namespace Waves.NET.Json
{
    public class TransactionInfoJsonConverter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            TransactionInfo? tInfo = null;

            if (reader.TokenType == JsonToken.StartObject)
            {
                var jObj = JObject.Load(reader);
                var type = jObj.Value<int>("type");
                var appStatus = jObj.Value<string>("applicationStatus");
                var height = jObj.Value<int>("height");

                switch (type)
                {
                    case 1: tInfo = new GenesisTransactionInfo(ReadTransaction<GenesisTransaction>(jObj, serializer), appStatus, height); break;
                    case 3: tInfo = new IssueTransactionInfo(ReadTransaction<IssueTransaction>(jObj, serializer), appStatus, height); break;
                    case 4: tInfo = new TransferTransactionInfo(ReadTransaction<TransferTransaction>(jObj, serializer), appStatus, height); break;
                    case 5: tInfo = new ReissueTransactionInfo(ReadTransaction<ReissueTransaction>(jObj, serializer), appStatus, height); break;
                    case 6: tInfo = new BurnTransactionInfo(ReadTransaction<BurnTransaction>(jObj, serializer), appStatus, height); break;
                    case 7: tInfo = new ExchangeTransactionInfo(ReadTransaction<ExchangeTransaction>(jObj, serializer), appStatus, height); break;
                    case 8: tInfo = new LeaseTransactionInfo(ReadTransaction<LeaseTransaction>(jObj, serializer), appStatus, height); break;
                    case 9: tInfo = new LeaseCancelTransactionInfo(ReadTransaction<LeaseCancelTransaction>(jObj, serializer), appStatus, height); break;
                    case 10: tInfo = new CreateAliasTransactionInfo(ReadTransaction<CreateAliasTransaction>(jObj, serializer), appStatus, height); break;
                    case 11: tInfo = new MassTransferTransactionInfo(ReadTransaction<MassTransferTransaction>(jObj, serializer), appStatus, height); break;
                    case 12: tInfo = new DataTransactionInfo(ReadTransaction<DataTransaction>(jObj, serializer), appStatus, height); break;
                    case 13: tInfo = new SetScriptTransactionInfo(ReadTransaction<SetScriptTransaction>(jObj, serializer), appStatus, height); break;
                    case 14: tInfo = new SponsorFeeTransactionInfo(ReadTransaction<SponsorFeeTransaction>(jObj, serializer), appStatus, height); break;
                    case 15: tInfo = new SetAssetScriptTransactionInfo(ReadTransaction<SetAssetScriptTransaction>(jObj, serializer), appStatus, height); break;
                    case 16: tInfo = new InvokeScriptTransactionInfo(ReadTransaction<InvokeScriptTransaction>(jObj, serializer), appStatus, height); break;
                    case 17: tInfo = new UpdateAssetInfoTransactionInfo(ReadTransaction<UpdateAssetInfoTransaction>(jObj, serializer), appStatus, height); break;
                }
            }

            return tInfo;
        }

        private T ReadTransaction<T>(JObject jO, JsonSerializer serializer) where T : Waves.NET.Transactions.Transaction, new()
        {
            T transaction = new();
            serializer.Populate(jO.CreateReader(), transaction);
            return transaction;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableTo(typeof(TransactionProto));
        }
    }
}
