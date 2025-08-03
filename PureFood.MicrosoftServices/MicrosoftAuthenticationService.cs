using Microsoft.Extensions.Logging;
using PureFood.Common;
using PureFood.HttpClientBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.MicrosoftServices
{
    public class MicrosoftAuthenticationService(
    IHttpClient httpClient,
    string clientId,
    string clientSecret,
    string loginUrl,
    string returnUrl,
    string tokenUrl,
    string getUserInfoUrl,
    ILogger<MicrosoftAuthenticationService> logger
) : IMicrosoftAuthenticationService
    {
        private const string EncryptionKey = "bjW4H#;Y+$N'z3vUD*e:(}eZLIIyov>4";

        public string GetLoginUrl(string sessionKey, out string state)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(loginUrl);
            builder.Append($"?client_id={clientId}");
            builder.Append("&response_type=code");
            builder.Append($"&redirect_uri={WebUtility.UrlEncode(returnUrl)}");
            builder.Append($"&scope={WebUtility.UrlEncode("https://graph.microsoft.com/user.read offline_access")}");
            state = EncryptionExtensions.Md5($"{sessionKey}{returnUrl}{EncryptionKey}{clientId}{clientSecret}");
            builder.Append($"&state={state}");
            return builder.ToString();
        }

        public async Task<TokenInfoResponse?> GetToken(string state, string code, string sessionKey,
            bool verifyState = true)
        {
            if (verifyState)
            {
                string stateCompare =
                    EncryptionExtensions.Md5($"{sessionKey}{returnUrl}{EncryptionKey}{clientId}{clientSecret}");
                if (state != stateCompare)
                {
                    throw new Exception("state invalid");
                }
            }

            var tokenRequestParameters = new Dictionary<string, string>()
        {
            { "client_id", clientId },
            { "redirect_uri", returnUrl },
            { "scope", "https://graph.microsoft.com/user.read offline_access" },
            { "client_secret", clientSecret },
            { "code", code },
            { "grant_type", "authorization_code" },
        };
            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, tokenUrl);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = requestContent;
            var response = await httpClient.SendAsync(requestMessage);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TokenInfoResponse? obj = Serialize.JsonDeserializeObject<TokenInfoResponse>(body);
                if (obj == null)
                {
                    logger.LogError("body convert to json failed: {Body}", body);
                }

                if (obj?.error?.Length > 0)
                {
                    logger.LogError("get token error: {Body}", body);
                }

                return obj;
            }

            var errorMessage =
                $"OAuth token endpoint failure: Status: {response.StatusCode};Headers: {response.Headers};Body: {body};";
            logger.LogError("{Message}", errorMessage);
            return null;
        }

        public async Task<MicrosoftUserInfo?> GetUserInfo(string accountToken)
        {
            string url = getUserInfoUrl; //"https://graph.microsoft.com/v1.0/me";
            HttpResponseMessage googleResponse = await httpClient.Get(url, accountToken);
            if (googleResponse.IsSuccessStatusCode)
            {
                string data = await googleResponse.Content.ReadAsStringAsync();
                var obj = Serialize.JsonDeserializeObject<MicrosoftUserInfo>(data);
                return obj;
            }

            return null;
        }

        public async Task<TokenInfoResponse?> RefreshToken(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return null;
            }
            var tokenRequestParameters = new Dictionary<string, string>()
        {
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken },
            { "redirect_uri", returnUrl }
        };

            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, tokenUrl);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = requestContent;

            var response = await httpClient.SendAsync(requestMessage);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return Serialize.JsonDeserializeObject<TokenInfoResponse>(body);
            }

            logger.LogError("Refresh token failed: {Body}", body);
            return null;
        }
    }
}
