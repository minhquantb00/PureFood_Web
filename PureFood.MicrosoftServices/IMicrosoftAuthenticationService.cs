using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.MicrosoftServices
{
    public interface IMicrosoftAuthenticationService
    {
        string GetLoginUrl(string sessionKey, out string state);
        Task<TokenInfoResponse?> GetToken(string state, string code, string sessionKey, bool verifyState = true);
        Task<MicrosoftUserInfo?> GetUserInfo(string accountToken);
        Task<TokenInfoResponse?> RefreshToken(string refreshToken);
    }
}
