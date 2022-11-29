using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Waves;
using WavesLabs.Node.Client.Transactions;
using WavesLabs.Node.Client.Transactions.Info;
using WavesLabs.Node.Transactions;

namespace WavesLabs.Node.Client.Json
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
                var height = jObj.Value<int>("height");

                switch (type)
                {
                    case 1: tInfo = new GenesisTransactionInfo(ReadTransaction<GenesisTransaction>(jObj, serializer), null, height); break;
                    case 2: tInfo = new PaymentTransactionInfo(ReadTransaction<PaymentTransaction>(jObj, serializer), null, height); break;
                    case 3: tInfo = new IssueTransactionInfo(ReadTransaction<IssueTransaction>(jObj, serializer), null, height); break;
                    case 4: tInfo = new TransferTransactionInfo(ReadTransaction<TransferTransaction>(jObj, serializer), null, height); break;
                    case 5: tInfo = new ReissueTransactionInfo(ReadTransaction<ReissueTransaction>(jObj, serializer), null, height); break;
                    case 6: tInfo = new BurnTransactionInfo(ReadTransaction<BurnTransaction>(jObj, serializer), null, height); break;
                    case 7: tInfo = new ExchangeTransactionInfo(ReadTransaction<ExchangeTransaction>(jObj, serializer), null, height); break;
                    case 8: tInfo = new LeaseTransactionInfo(ReadTransaction<LeaseTransaction>(jObj, serializer), null, height); break;
                    case 9: tInfo = new LeaseCancelTransactionInfo(ReadTransaction<LeaseCancelTransaction>(jObj, serializer), null, height); break;
                    case 10: tInfo = new CreateAliasTransactionInfo(ReadTransaction<CreateAliasTransaction>(jObj, serializer), null, height); break;
                    case 11: tInfo = new MassTransferTransactionInfo(ReadTransaction<MassTransferTransaction>(jObj, serializer), null, height); break;
                    case 12: tInfo = new DataTransactionInfo(ReadTransaction<DataTransaction>(jObj, serializer), null, height); break;
                    case 13: tInfo = new SetScriptTransactionInfo(ReadTransaction<SetScriptTransaction>(jObj, serializer), null, height); break;
                    case 14: tInfo = new SponsorFeeTransactionInfo(ReadTransaction<SponsorFeeTransaction>(jObj, serializer), null, height); break;
                    case 15: tInfo = new SetAssetScriptTransactionInfo(ReadTransaction<SetAssetScriptTransaction>(jObj, serializer), null, height); break;
                    case 16: tInfo = new InvokeScriptTransactionInfo(ReadTransaction<InvokeScriptTransaction>(jObj, serializer), null, height); break;
                    case 17: tInfo = new UpdateAssetInfoTransactionInfo(ReadTransaction<UpdateAssetInfoTransaction>(jObj, serializer), null, height); break;
                    case 18: tInfo = new EthereumTransactionInfo(ReadTransaction<EthereumTransaction>(jObj, serializer), null, height); break;
                }
            }

            return tInfo;
        }

        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) => throw new NotImplementedException();
        public override bool CanConvert(Type objectType) => objectType.IsAssignableTo(typeof(TransactionProto));

        private T ReadTransaction<T>(JObject jO, JsonSerializer serializer) where T : Transaction, new()
        {
            T transaction = new();
            serializer.Populate(jO.CreateReader(), transaction);
            return transaction;
        }
    }
}
