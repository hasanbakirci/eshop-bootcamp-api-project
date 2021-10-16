using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using eshop.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eshop.API.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOption>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization")){
                return Task.FromResult(AuthenticateResult.NoResult());
            }
            if(!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"],out AuthenticationHeaderValue headerValue)){
                return Task.FromResult(AuthenticateResult.NoResult());
            }
            if(headerValue.Scheme != "Basic"){
                return Task.FromResult(AuthenticateResult.NoResult());
            }
            byte[] incomingEncodedData = Convert.FromBase64String(headerValue.Parameter);
            string incomingText = Encoding.UTF8.GetString(incomingEncodedData);
            string username = incomingText.Split(':')[0];
            string password = incomingText.Split(':')[1];

            var user = new UserService().IsValid(username,password);
            if(user == null){
                return Task.FromResult(AuthenticateResult.Fail("Username or Password invalid"));
            }
            Claim[] claims = new[]{
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, Scheme.Name);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal,Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}