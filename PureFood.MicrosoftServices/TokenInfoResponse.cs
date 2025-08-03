using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.MicrosoftServices
{
    public record TokenInfoResponse(
    string token_type,
    string scope,
    int expires_in,
    int ext_expires_in,
    string? access_token,
    string refresh_token,
    string error,
    string error_description,
    int[] error_codes,
    DateTime timestamp,
    string trace_id,
    string correlation_id,
    string error_uri
);

    public record MicrosoftUserInfo(
        string _odata_context,
        string userPrincipalName,
        string id,
        string displayName,
        string surname,
        string givenName,
        string preferredLanguage,
        string mail,
        object mobilePhone,
        object jobTitle,
        object officeLocation,
        object[] businessPhones
    );
}
