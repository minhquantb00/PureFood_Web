using Microsoft.Extensions.Logging;
using PureFood.Common;
using PureFood.HttpClientBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ZaloServices
{
    public class ZaloAuthenticationService(
    IHttpClient httpClient,
    string clientId,
    string clientSecret,
    string loginUrl,
    string returnUrl,
    string tokenUrl,
    string tokenInfoUrl,
    string getUserDetailWithOAUrl,
    string oaUserDetailUrl,
    ILogger<ZaloAuthenticationService> logger
) : IZaloAuthenticationService
    {
        private const string EncryptionKey = "bjW4H#;Y+$N'z3vUD*e:(}eZLIIyov>4";

        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .TrimEnd('=') // Remove padding
                .Replace('+', '-') // Make URL safe
                .Replace('/', '_');
        }

        private static string GenerateCodeVerifier(int length)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[length];
                rng.GetBytes(bytes);

                string verifier = Base64UrlEncode(bytes);
                return verifier;
            }
        }

        private static string GenerateCodeChallenge(string codeVerifier)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.ASCII.GetBytes(codeVerifier);
                byte[] hash = sha256.ComputeHash(bytes);

                string challenge = Base64UrlEncode(hash);
                return challenge;
            }
        }

        public string GetLoginUrl(string sessionKey, out string code_challenge, out string state)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(loginUrl);
            builder.Append($"?app_id={clientId}");
            builder.Append($"&redirect_uri={returnUrl}");
            code_challenge = GenerateCodeChallenge(GenerateCodeVerifier(43));
            // const int length = 43;
            // Random random = Random.secure();
            // String verifier =
            //     base64UrlEncode(List<int>.generate(length, (_) => random.nextInt(256)))
            //         .split('=')[0];
            // String rs =
            //     base64UrlEncode(sha256.convert(ascii.encode(codeVerifier)).bytes)
            //         .split('=')[0];        
            builder.Append($"&code_challenge={code_challenge}");
            state = EncryptionExtensions.Md5($"{sessionKey}{returnUrl}{EncryptionKey}{clientId}{clientSecret}");
            builder.Append($"&state={state}");
            return builder.ToString();
        }

        public async Task<TokenInfoResponse?> GetToken(string code, string state, string code_verifier, string sessionKey,
            bool verifyState = true)
        {
            // if (verifyState)
            // {
            //     string stateCompare =
            //         EncryptionExtensions.Md5($"{sessionKey}{returnUrl}{EncryptionKey}{clientId}{clientSecret}");
            //     if (state != stateCompare)
            //     {
            //         throw new Exception("state invalid");
            //     }
            // }


            var tokenRequestParameters = new Dictionary<string, string>()
        {
            { "app_id", clientId },
            { "secret_key", clientSecret },
            { "code", code },
            { "grant_type", "authorization_code" },
        };
            if (verifyState)
            {
                tokenRequestParameters.Add("code_verifier", code_verifier);
            }

            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, tokenUrl);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Headers.Add("secret_key", clientSecret);
            requestMessage.Content = requestContent;
            var response = await httpClient.SendAsync(requestMessage);
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TokenInfoResponse? obj = Serialize.JsonDeserializeObject<TokenInfoResponse>(body);
                return obj;
            }

            var errorMessage =
                $"OAuth token endpoint failure: Status: {response.StatusCode};Headers: {response.Headers};Body: {body};";
            logger.LogError("{Message}", errorMessage);
            return null;
        }

        public async Task<ZaloInfoResponse?> GetUserInfo(string accountToken)
        {
            string url = tokenInfoUrl; //"https://graph.microsoft.com/v1.0/me";
            Dictionary<string, string>? headers = new Dictionary<string, string>();
            headers.Add("access_token", $"{accountToken}");
            HttpResponseMessage googleResponse = await httpClient.Get(url, headers);
            if (googleResponse.IsSuccessStatusCode)
            {
                string data = await googleResponse.Content.ReadAsStringAsync();
                var obj = Serialize.JsonDeserializeObject<ZaloInfoResponse>(data);
                return obj;
            }

            return null;
        }

        public async Task<TokenInfoResponse?> RefreshToken(string refreshToken)
        {
            var tokenRequestParameters = new Dictionary<string, string>()
        {
            { "app_id", clientId },
            { "grant_type", "refresh_token" },
            { "refresh_token", refreshToken }
        };

            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, tokenUrl);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Headers.Add("secret_key", clientSecret);
            requestMessage.Content = requestContent;

            var response = await httpClient.SendAsync(requestMessage);
            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                TokenInfoResponse? obj = Serialize.JsonDeserializeObject<TokenInfoResponse>(body);
                if (obj == null || string.IsNullOrEmpty(obj.access_token))
                {
                    logger.LogError("Deserialize failed: {Body}", body);
                }
                return obj;
            }

            var errorMessage =
                $"Zalo refresh token failure: Status: {response.StatusCode};Headers: {response.Headers};Body: {body};";
            logger.LogError("{Message}", errorMessage);
            return null;
        }
    }
}
