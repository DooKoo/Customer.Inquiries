using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Customer.Inquiries.Web.Security
{
    public class ApiKeyAuthorizationHandler : AuthenticationHandler<ApiKeyAuthorizationOptions>
    {
        public const string AUTH_SCHEME_NAME = "ApiKey";
        public const string HEADER_NAME = "Authorization";
        public ApiKeyAuthorizationHandler(IOptionsMonitor<ApiKeyAuthorizationOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authValue = "";
            bool authenticated = Context.User.Identity.IsAuthenticated;

            if (!authenticated)
            {
                if (!Context.Request.Headers.TryGetValue(HEADER_NAME, out var headerValue))
                {
                    return AuthenticateResult.NoResult();
                }

                var headerValueAsString = headerValue.ToString();

                if (string.IsNullOrWhiteSpace(headerValueAsString) || (!headerValueAsString.StartsWith(AUTH_SCHEME_NAME)))
                {
                    return AuthenticateResult.NoResult();
                }
                // Remove the scheme and trim
                authValue = headerValueAsString.Substring(AUTH_SCHEME_NAME.Length).Trim();
            }

            ClaimsIdentity identity;
            if (this.Options.ApiKeys.Contains(authValue) || authenticated)
            {
                // Create an identity with role Application
                var claims = new List<Claim>() { new Claim("role", "application") };
                identity = new ClaimsIdentity(claims, AUTH_SCHEME_NAME, "name", "role");
            }
            else
            {
                return AuthenticateResult.Fail("Wrong api key");
            }

            var authProperties = new AuthenticationProperties();
            var authenticationTicket = new AuthenticationTicket(new ClaimsPrincipal(identity), authProperties, AUTH_SCHEME_NAME);

            return AuthenticateResult.Success(authenticationTicket);
        }
    }

    public static class ApiKeyAuthenticationExtensions
    {
        public static AuthenticationBuilder AddApiKeyAuthenticationMiddleware(this AuthenticationBuilder builder, Action<ApiKeyAuthorizationOptions> configureOptions)
        {
            return builder.AddScheme<ApiKeyAuthorizationOptions, ApiKeyAuthorizationHandler>(ApiKeyAuthorizationHandler.AUTH_SCHEME_NAME, ApiKeyAuthorizationHandler.AUTH_SCHEME_NAME, configureOptions);
        }
    }
}
