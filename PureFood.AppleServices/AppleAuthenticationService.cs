using PureFood.Common;
using PureFood.HttpClientBase;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AppleServices
{
    public class AppleAuthenticationService : IAppleAuthenticationService
    {
        private const string EncryptionKey = "bjW4H#;Y+$N'z3vUD*e:(}eZLIIyov>4";
        private readonly string _clientId;
        private readonly string _keyId;
        private readonly string _teamId;
        private readonly string _privateKey;

        private readonly string _loginUrl;
        private readonly string _returnUrl;
        private readonly string _getTokenUrl;
        private readonly string _getUserInfoUrl;
        private readonly IHttpClient _httpClient;

        public AppleAuthenticationService(string clientId, string keyId, string teamId, string privateKey,
            string loginUrl, string returnUrl,
            string getTokenUrl, string getUserInfoUrl, IHttpClient httpClient)
        {
            _clientId = clientId;
            _loginUrl = loginUrl;
            _returnUrl = returnUrl;
            _getTokenUrl = getTokenUrl;
            _getUserInfoUrl = getUserInfoUrl;
            _httpClient = httpClient;
            _keyId = keyId;
            _teamId = teamId;
            _privateKey = privateKey;
        }

        public string GetLoginUrl(string sessionKey, out string state)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_loginUrl);
            builder.Append($"?client_id={_clientId}");
            builder.Append($"&scope=name%20email");
            builder.Append($"&response_type=code id_token");
            builder.Append("&redirect_uri=" + WebUtility.UrlEncode(_returnUrl));
            state = EncryptionExtensions.Md5(sessionKey + _returnUrl + EncryptionKey + _clientId + _privateKey);
            builder.Append($"&state={state}");
            builder.Append("&response_mode=form_post");

            return builder.ToString();
        }

        public async Task<AppleGetTokenResponse?> GetToken(string state, string code, string sessionKey, bool verifyState = true)
        {
            if (verifyState)
            {
                string stateCompare =
                    EncryptionExtensions.Md5(sessionKey + _returnUrl + EncryptionKey + _clientId + _privateKey);
                if (state != stateCompare)
                {
                    throw new Exception("state invalid");
                }
            }
            AppleClientSecretGenerator appleClientSecretGenerator = new AppleClientSecretGenerator();
            string clientSecret =
                await appleClientSecretGenerator.GenerateAsync(_keyId, _clientId, _teamId, _privateKey);
            var tokenRequestParameters = new Dictionary<string, string>()
        {
            { "client_id", _clientId },
            { "redirect_uri", _returnUrl },
            { "client_secret", clientSecret },
            { "code", code },
            { "grant_type", "authorization_code" },
        };
            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);
            string url = _getTokenUrl;
            var response = await _httpClient.Post(url, requestContent);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                AppleGetTokenResponse? model = Serialize.JsonDeserializeObject<AppleGetTokenResponse>(content);
                return model;
            }

            throw new Exception($"Get Token fail:{response.StatusCode}---{response.ReasonPhrase}");
        }

        public (string? Sub, string? Email) GetUserInfo(string idToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(idToken);
            string? sub = securityToken?.Claims.FirstOrDefault(p => p.Type.ToLower() == "sub")?.Value;
            string? email = securityToken?.Claims.FirstOrDefault(p => p.Type.ToLower() == "email")?.Value;
            return (sub, email);
        }
    }
}
