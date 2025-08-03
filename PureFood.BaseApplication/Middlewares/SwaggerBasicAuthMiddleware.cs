using PureFood.Common;
using PureFood.Config;
using System.Net;
using System.Text;

namespace PureFood.BaseApplication.Middlewares
{
    public class SecureSwaggerMiddleware(RequestDelegate next)
    {
        private static readonly HashSet<string> WhiteListedExtensions = [".css", ".js", ".png", ".gif", ".jpg", ".jpeg"];

        public async Task Invoke(HttpContext context, ILogger<SecureSwaggerMiddleware> logger)
        {
            var useSwagger = ConfigSettingEnum.UseSwagger.GetConfig().AsInt() == 1;
            if (!useSwagger)
            {
                await next.Invoke(context);
                return;
            }

            var uriComponent = context.Request.Path.ToUriComponent();
            if (!uriComponent.Contains("swagger"))
            {
                await next.Invoke(context);
                return;
            }

            if (WhiteListedExtensions.Any(extension => uriComponent.EndsWith(extension)))
            {
                await next.Invoke(context);
                return;
            }

            string? authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                //Extract credentials
                var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                var encoding = Encoding.GetEncoding("iso-8859-1");
                var usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                var seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);

                string usernameConfig = ConfigSettingEnum.SwaggerUsername.GetConfig();
                string passwordConfig = ConfigSettingEnum.SwaggerPassword.GetConfig();
                if (username == usernameConfig && password == passwordConfig)
                {
                    await next.Invoke(context);
                    return;
                }
            }

            // Authorization failed
            context.Response.Headers.Append("WWW-Authenticate", "Basic");
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }

    public static class AuthorizedSwaggerExtension
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecureSwaggerMiddleware>();
        }
    }
}
