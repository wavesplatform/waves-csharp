using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace WavesLabs.Node.Transactions.Utils
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
                throw new ArgumentNullException("Argument 'jsonToDeserialize' cannot be null, empty or whitespace.");
            }

            return JsonConvert.DeserializeObject<T>(jsonToDeserialize);
        }

        public static (bool Succees, T? Result, Exception? Error) TryDeserialize<T>(string jsonToDeserialize)
        {
            try
            {
                return (true, Deserialize<T>(jsonToDeserialize), null);
            }
            catch(Exception ex)
            {
                return (false, default(T), ex);
            }
        }

        //public static (bool Succees, T? Result) TryDeserialize<T>(string jsonToDeserialize)
        //{
        //    if (string.IsNullOrWhiteSpace(jsonToDeserialize))
        //    {
        //        throw new ArgumentNullException("Argument 'jsonToDeserialize' cannot be null, empty or whitespace");
        //    }

        //    try
        //    {
        //        return (true, JsonConvert.DeserializeObject<T>(jsonToDeserialize));
        //    }
        //    catch
        //    {
        //        return (false, default(T));
        //    }
        //}
    }
}
