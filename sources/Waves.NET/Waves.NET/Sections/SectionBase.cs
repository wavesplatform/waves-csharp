using Newtonsoft.Json;
using System.Text;
using Waves.NET.Exceptions;

namespace Waves.NET
{
    public abstract class SectionBase
    {
        protected string Route { get; init; }

        protected HttpClient HttpClient { get; init; }

        internal SectionBase(HttpClient httpClient, string subRoute)
        {
            HttpClient = httpClient;
            Route = subRoute;
        }

        protected TResult PublicRequest<TResult>(HttpMethod method, string? url = null, string? jsonBody = null)
        {
            var requestUrl = Route + (string.IsNullOrWhiteSpace(url) ? "" : $"/{url}");

            using (var request = new HttpRequestMessage(method, requestUrl))
            {
                if (!string.IsNullOrWhiteSpace(jsonBody))
                {
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                }

                return ProcessRequest<TResult>(request, false);
            }
        }

        private TResult ProcessRequest<TResult>(HttpRequestMessage request, bool readAsStream)
        {
            var response = HttpClient.Send(request);

            return readAsStream
                ? ProcessResponseReadStreamed<TResult>(response)
                : ProcessResponseReadStringed<TResult>(response);
        }

        private TResult ProcessResponseReadStreamed<TResult>(HttpResponseMessage response, JsonConverter? jsonConverter = null)
        {
            var jsonStream = response.Content.ReadAsStream();

            using var sr = new StreamReader(jsonStream);
            using var jsonReader = new JsonTextReader(sr);
            var serializer = new JsonSerializer();

            if (jsonConverter is not null) {
                serializer.Converters.Add(jsonConverter);
            }

            if (response.IsSuccessStatusCode)
            {
                return serializer.Deserialize<TResult>(jsonReader)!;
            }

            var apiError = serializer.Deserialize<ApiError>(jsonReader);
            throw new NodeException(apiError!);
        }

        private TResult ProcessResponseReadStringed<TResult>(HttpResponseMessage response)
        {
            var jsonString = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResult>(jsonString)!;
            }

            var apiError = JsonConvert.DeserializeObject<ApiError>(jsonString);
            throw new NodeException(apiError!);
        }
    }
}
