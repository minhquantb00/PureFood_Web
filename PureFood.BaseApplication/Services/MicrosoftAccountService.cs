using PureFood.BaseApplication.Models;
using PureFood.Common;
using PureFood.HttpClientBase;
using System.Net.Http.Headers;

namespace PureFood.BaseApplication.Services
{
    public class MicrosoftAccountService(IHttpClient httpClient, string url)
    {
        // url = "https://graph.microsoft.com/v1.0/me"
        public async Task<MicrosoftTokenResponse?> Get(string accountToken)
        {
            using var graphHttpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            graphHttpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accountToken);

            HttpResponseMessage graphHttpResponse = await httpClient.SendAsync(graphHttpRequest, HttpCompletionOption.ResponseContentRead, CancellationToken.None);
            graphHttpResponse.EnsureSuccessStatusCode();

            if (graphHttpResponse.IsSuccessStatusCode)
            {
                // var graphResponseBody = JsonDocument.Parse(await graphHttpResponse.Content.ReadAsStringAsync());
                MicrosoftTokenResponse? obj = Serialize.JsonDeserializeObject<MicrosoftTokenResponse>(await graphHttpResponse.Content.ReadAsStringAsync());
                return obj;
            }
            return null;
        }
    }
}
