using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Authentication
{
    /*
     * This class is used to manage the API Basic Authentication
     */
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private static string VALID_USERNAME = "teste@netcore.com";
        private static string VALID_PASSWORD = "1234";

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Basic ".Length).Trim();
                var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var credentials = credentialstring.Split(':');
                if (credentials[0] == VALID_USERNAME && credentials[1] == VALID_PASSWORD)
                {
                    var claims = new[] { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, "NetCoreDeveloper") };
                    var identity = new ClaimsIdentity(claims, "Basic");
                    context.User = new ClaimsPrincipal(identity);
                }
                else
                {
                    await InvalidAuthentication(context);
                    return;
                }
            }
            else
            {
                await InvalidAuthentication(context);
                return;
            }
            await _next(context);
        }

        private async Task InvalidAuthentication(HttpContext context)
        {
            context.Response.StatusCode = 401;
            context.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"dotnetthoughts.net\"");
            context.Response.ContentType = "application/json";

            var jsonString = "{\"Message\": \"Autorização de acesso a API negada.\",\"Authenticated\":false}";
            byte[] data = Encoding.UTF8.GetBytes(jsonString);

            await context.Response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}
