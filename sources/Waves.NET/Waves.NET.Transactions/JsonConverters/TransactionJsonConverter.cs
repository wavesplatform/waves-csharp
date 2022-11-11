using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Waves.NET.Transactions.JsonConverters
{
    public class TransactionJsonConverter : JsonConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            Transaction? transaction = null;

            if (reader.TokenType == JsonToken.StartObject)
            {
                var jO = JObject.Load(reader);
                var type = jO.Value<int>("type");

                switch (type)
                {
                    case 1: transaction = new GenesisTransaction(); break;
                    case 3: transaction = new IssueTransaction(); break;
                    case 4: transaction = new TransferTransaction(); break;
                    case 5: transaction = new ReissueTransaction(); break;
                    case 6: transaction = new BurnTransaction(); break;
                    case 7: transaction = new ExchangeTransaction(); break;
                    case 8: transaction = new LeaseTransaction(); break;
                    case 9: transaction = new LeaseCancelTransaction(); break;
                    case 10: transaction = new CreateAliasTransaction(); break;
                    case 11: transaction = new MassTransferTransaction(); break;
                    case 12: transaction = new DataTransaction(); break;
                    case 13: transaction = new SetScriptTransaction(); break;
                    case 14: transaction = new SponsorFeeTransaction(); break;
                    case 15: transaction = new SetAssetScriptTransaction(); break;
                    case 16: transaction = new InvokeScriptTransaction(); break;
                    case 17: transaction = new UpdateAssetInfoTransaction(); break;
                }

                if(transaction is not null)
                {
                    serializer.Populate(jO.CreateReader(), transaction);
                }
            }

            return transaction;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableTo(typeof(Transaction));
        }
    }
}
