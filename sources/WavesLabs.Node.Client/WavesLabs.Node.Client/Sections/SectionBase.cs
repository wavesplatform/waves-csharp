using System.Text;
using WavesLabs.Node.Client.Exceptions;
using WavesLabs.Node.Transactions.Utils;

namespace WavesLabs.Node.Client.Sections
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

                var response = HttpClient.Send(request);
                var jsonString = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    return JsonUtils.Deserialize<TResult>(jsonString)!;
                }

                var (Succees, Result, _) = JsonUtils.TryDeserialize<ApiError>(jsonString);
                throw Succees
                    ? new NodeException(Result!)
                    : new HttpException(response.StatusCode, response.ReasonPhrase!);
            }
        }
    }
}
