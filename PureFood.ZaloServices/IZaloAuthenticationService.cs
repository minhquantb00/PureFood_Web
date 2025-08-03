using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ZaloServices
{
    public interface IZaloAuthenticationService
    {
        string GetLoginUrl(string sessionKey, out string code_challenge, out string state);
        Task<TokenInfoResponse?> GetToken(string code, string state, string code_verifier, string sessionKey, bool verifyState = true);
        Task<ZaloInfoResponse?> GetUserInfo(string accountToken);
        Task<TokenInfoResponse?> RefreshToken(string refreshToken);
    }
}
