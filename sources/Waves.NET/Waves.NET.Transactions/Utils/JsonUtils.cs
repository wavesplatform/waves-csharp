using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Waves.NET.Transactions.Utils
{
    public static class JsonUtils
    {
        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new List<JsonConverter> { new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() } }
        };

        public static string Serialize(object objectToSerialize)
        {
            if(objectToSerialize is null)
            {
                throw new ArgumentNullException("Argument 'objectToSerialize' cannot be null.");
            }

            return JsonConvert.SerializeObject(objectToSerialize, _jsonSerializerSettings);
        }

        public static T? Deserialize<T>(string jsonToDeserialize) {
            if(string.IsNullOrWhiteSpace(jsonToDeserialize))
            {
                throw new ArgumentNullException("Argument 'jsonToDeserialize' cannot be null.");
            }
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonToDeserialize);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
