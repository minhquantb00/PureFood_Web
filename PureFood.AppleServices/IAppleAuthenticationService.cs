using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.AppleServices
{
    public interface IAppleAuthenticationService
    {
        string GetLoginUrl(string sessionKey, out string state);
        Task<AppleGetTokenResponse?> GetToken(string state, string code, string sessionKey, bool verifyState = true);
        (string? Sub, string? Email) GetUserInfo(string accountToken);
    }
}
