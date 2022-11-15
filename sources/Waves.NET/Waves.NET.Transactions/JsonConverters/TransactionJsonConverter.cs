using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Waves.NET.Transactions.TransactionData;

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

                switch ((TransactionType)type)
                {
                    case TransactionType.Genesis: transaction = new GenesisTransaction(); break;
                    case TransactionType.Issue: transaction = new IssueTransaction(); break;
                    case TransactionType.Transfer: transaction = new TransferTransaction(); break;
                    case TransactionType.Reissue: transaction = new ReissueTransaction(); break;
                    case TransactionType.Burn: transaction = new BurnTransaction(); break;
                    case TransactionType.Exchange: transaction = new ExchangeTransaction(); break;
                    case TransactionType.Lease: transaction = new LeaseTransaction(); break;
                    case TransactionType.LeaseCancel: transaction = new LeaseCancelTransaction(); break;
                    case TransactionType.CreateAlias: transaction = new CreateAliasTransaction(); break;
                    case TransactionType.MassTransfer: transaction = new MassTransferTransaction(); break;
                    case TransactionType.Data: transaction = new DataTransaction(); break;
                    case TransactionType.SetScript: transaction = new SetScriptTransaction(); break;
                    case TransactionType.SponsorFee: transaction = new SponsorFeeTransaction(); break;
                    case TransactionType.SetAssetScript: transaction = new SetAssetScriptTransaction(); break;
                    case TransactionType.InvokeScript: transaction = new InvokeScriptTransaction(); break;
                    case TransactionType.UpdateAssetInfo: transaction = new UpdateAssetInfoTransaction(); break;
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
