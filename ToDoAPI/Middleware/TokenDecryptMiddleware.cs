using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Primitives;

namespace ToDoAPI.Middleware
{
    public class TokenDecryptMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenDecryptMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        /** descrypt the token lied inside the cookies if there is any in the incoming HttpRequest,then put the new decrypted token to authorization header for HttpRequest. This middleware should be placed before the authentication middleware  */

        public async Task Invoke(HttpContext context, IDataProtectionProvider protectProvider)
        {
            var dataProtector = protectProvider.CreateProtector("brearer_token_in_cookie.v1");
            var authorizationHeader = context.Request.Headers["authorization"];
            if (context.Request.Headers["authorization"] == StringValues.Empty)
            {
                var token = context.Request.Cookies["token"];
                if (token != null)
                {
                    var decryptedToken = dataProtector.Unprotect(token);
                    context.Request.Headers.Add("authorization", decryptedToken);
                }
            }
            await _next(context);
        }
    }
}