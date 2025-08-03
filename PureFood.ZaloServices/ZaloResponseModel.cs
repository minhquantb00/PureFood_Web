using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ZaloServices
{
    public class ZaloAuthenticationResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    public record TokenInfoResponse(
        string token_type,
        string scope,
        int expires_in,
        int ext_expires_in,
        string access_token,
        string refresh_token,
        int? error,
        string? message
    );

    public record ZaloInfoResponse(
        bool is_sensitive,
        string name,
        string id,
        int error,
        string message,
        Picture picture
    );

    public record Picture(
        Data data
    );

    public record Data(
        string url
    );
}
